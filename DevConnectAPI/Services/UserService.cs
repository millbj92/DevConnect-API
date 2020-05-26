using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DevConnectAPI.Models;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using DevConnectAPI.Services.Security;
using System.Text;
using System.Security.Principal;

namespace DevConnectAPI.Services
{
    public interface IUserService
    {
        User Authenticate(User user);
        string GetEmailToken(User user);
        bool ValidateToken(string token, User user);
    }

    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public User Authenticate(User user)
         {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.user_id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "DevConnect",
                Audience = user.user_id.ToString()
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }

        public string GetEmailToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.email.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "DevConnect",
                Audience = user.user_id.ToString()
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return(tokenHandler.WriteToken(token));
        }

        public bool ValidateToken(string token, User user)
        {
            var handler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;

            try
            {
                IPrincipal principal = handler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateLifetime = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidIssuer = "DevConnect",
                    ValidAudience = user.user_id.ToString(),
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_appSettings.Secret))
                }, out validatedToken);
            }
            catch(Exception ex)
            {
                return false;
            }
            

            return validatedToken != null;
        }

    }
}
