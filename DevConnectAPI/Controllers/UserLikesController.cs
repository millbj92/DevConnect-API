using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DevConnectAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace DevConnectAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserLikesController : ControllerBase
    {
        private readonly DevConnectContext _context;

        public UserLikesController(DevConnectContext context)
        {
            _context = context;
        }

        // GET: api/UserLikes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLike>>> GetUserLikes()
        {
            return await _context.UserLikes.ToListAsync();
        }

        // GET: api/UserLikes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserLike>> GetUserLike(int id)
        {
            var userLike = await _context.UserLikes.FindAsync(id);

            if (userLike == null)
            {
                return NotFound();
            }

            return userLike;
        }

        // PUT: api/UserLikes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserLike(int id, UserLike userLike)
        {
            if (id != userLike.like_id)
            {
                return BadRequest();
            }

            _context.Entry(userLike).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLikeExists(id))
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

        // POST: api/UserLikes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<UserLike>> PostUserLike(UserLike userLike)
        {
            _context.UserLikes.Add(userLike);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLike", new { id = userLike.like_id }, userLike);
        }

        // DELETE: api/UserLikes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserLike>> DeleteUserLike(int id)
        {
            var userLike = await _context.UserLikes.FindAsync(id);
            if (userLike == null)
            {
                return NotFound();
            }

            _context.UserLikes.Remove(userLike);
            await _context.SaveChangesAsync();

            return userLike;
        }

        private bool UserLikeExists(int id)
        {
            return _context.UserLikes.Any(e => e.like_id == id);
        }
    }
}
