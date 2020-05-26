using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevConnectAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using DevConnectAPI.Services.Security;
using Microsoft.AspNetCore.Authorization;
using DevConnectAPI.Services;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Principal;

namespace DevConnectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DevConnectContext _context;
        private readonly IPasswordHasher _hasher;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly AppSettings _appSettings;
        public UsersController(DevConnectContext context, IPasswordHasher hasher, IUserService userService, IEmailSender emailSender, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _hasher = hasher;
            _userService = userService;
            _emailSender = emailSender;
            _appSettings = appSettings.Value;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody]AuthenticateModel model)
        {
            string hash = _hasher.Hash(model.Password);
            //(bool Verified, bool NeedsUpgrade) = _hasher.Check(hash, model.Password);
            var authUser = await _context.Users.Where(x => x.email == model.Email).FirstOrDefaultAsync();
            var valid = _hasher.IsValid(model.Password, authUser.password);

            System.Diagnostics.Debug.WriteLine(hash + " / " + authUser.password);
            if (authUser == null || !valid)
                return NotFound();

            var user = _userService.Authenticate(authUser);
            var userDTO = new UserDTO()
            {
                user_id = user.user_id,
                email = user.email,
                userProfile = user.userProfile,
                friends_list = user.friends_list,
                Messages = user.Messages,
                Token = user.Token,
            };

            if (_context.UserStatuses.Any(x => x.User.user_id == user.user_id)) {
                var userStatus = await _context.UserStatuses.Where(x => x.User.user_id == user.user_id).FirstOrDefaultAsync();
                userStatus.Status = "ONLINE";
                _context.SaveChangesAsync();
            }

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(userDTO);
        }

        [AllowAnonymous]
        [HttpGet("confirm/{token}")]
        public async Task<IActionResult> Confirm(string token)
        {
            System.Diagnostics.Debug.WriteLine(token);
            var user = await _context.Users.Where(x => x.verificationToken == token).FirstOrDefaultAsync();
            if (user == null)
                return NotFound();

            if (!_userService.ValidateToken(token, user))
                return Unauthorized();

            user.isConfirmed = true;
            await PutUser(user.user_id, user);

            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("forgotpassword/{email}")]
        public async Task<IActionResult> Forgot(string email)
        {
            var user = await _context.Users.Where(x => x.email == email).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            user.verificationToken = _userService.GetEmailToken(user);
            await _context.SaveChangesAsync();

            string url = "http://localhost:4200/reset?token=" + user.verificationToken;
            await _emailSender.SendEmailAsync(user.email, "Password Reset", "Click the link below to reset your password: \n" + url);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("resetpassword/{token}")]
        public async Task<IActionResult> Reset([FromBody] AuthenticateModel resetuser, string token)
        {
            var user = await _context.Users.Where(x => x.verificationToken == token).FirstOrDefaultAsync();

            if (user == null)
                return NotFound();

            user.password = _hasher.Hash(resetuser.Password);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.user_id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(user.email))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
           
            if (UserExists(user.email))
            {
                return Conflict();
            }
            user.password = _hasher.Hash(user.password);
            
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return BadRequest();
            }

            user.verificationToken = _userService.GetEmailToken(user);
            await _context.SaveChangesAsync();

            string url = "http://localhost:4200/confirm?token=" + user.verificationToken;
            await _emailSender.SendEmailAsync(user.email, "Please confirm your account", "Thank you for registering with DevConnect! Before you can sign in you will need to register using the link below: \n" + url);

            var userDTO = new UserDTO()
            {
                user_id = user.user_id,
                email = user.email,
                userProfile = user.userProfile,
                friends_list = user.friends_list,
                Messages = user.Messages,
                Token = user.Token,
            };


            return Ok(userDTO);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(string email)
        {
            return _context.Users.Any(e => e.email == email);
        }
    }
}
