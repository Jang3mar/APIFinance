using FinanceAPI.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinanceAPI.Controllers
{
    public class AccountController
    {
        private readonly JWTSettings _options;

        public AccountController(IOptions<JWTSettings> optAccess)
        {
            _options = optAccess.Value;
        }

        private const string secretKey = "jwts-django-sdfkfdkuyh57udtf5j5u8f7fuyhdrjf8rgfjghtbg";
        private const string issuer = "MyApiToken";
        private const string audience = "MyClient";

        public static string GetToken()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Ivan"),
                new Claim("level", "123"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var singinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(singinKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GetTokenUser()
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Petr"),
                new Claim("level", "456"),
                new Claim(ClaimTypes.Role, "User")
            };

            var singinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(5)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(singinKey, SecurityAlgorithms.HmacSha256)
                );
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
