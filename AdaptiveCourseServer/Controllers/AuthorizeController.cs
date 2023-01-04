using AdaptiveCourseServer.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;

namespace AdaptiveCourseServer.Controllers
{
    public class AuthorizeController : Controller
    {
        private UserContext db;
        public AuthorizeController(UserContext context)
        {
            db = context;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromHeader]string Authorization)
        {
            byte[] encodedAuthorization = Convert.FromBase64String(Authorization.Replace("Authorization Basic ", ""));
            string decodedAuthoriation = Encoding.GetEncoding("ISO-8859-1").GetString(encodedAuthorization);
            string login = decodedAuthoriation.Split(':')[0]; 
            string password = decodedAuthoriation.Split(':')[1];
            User user = await db.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
            if (user != null)
            {
                var identity = await GetIdentity(login, password);

                DateTime now = DateTime.UtcNow;
                var jwt = new JwtSecurityToken(
                    issuer: JwtSettings.Issuer,
                    audience: JwtSettings.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(JwtSettings.LifeTime)),
                    signingCredentials: new SigningCredentials(JwtSettings.GetSymmetricSecurityKey(),
                        SecurityAlgorithms.HmacSha256));
                var encodeJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                return Ok(encodeJwt);
            }
            return Content("Wrong login or password");
        }

        private async Task<ClaimsIdentity> GetIdentity(string login, string password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, login)
            };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return claimsIdentity;
        }
    }
}
