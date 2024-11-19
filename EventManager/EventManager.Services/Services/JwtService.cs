using EventManager.Services.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManager.Services.Services
{
    public class JwtService : IJwtService
    {
        public string GenerateJwtToken(Guid userId, string username, List<string> roleNames)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("S8s5STbzFQ7itN0qA/ag8Sq+LNV9F2GXlR641LevMpGl0eKGaZhj/Dez8JUGo9oX"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            HashSet<Claim> claims = new()
            {
                new Claim("ID", $"{userId}"),
                new Claim("Username", username)
            };

            foreach (var roleName in roleNames)
                claims.Add(new Claim(ClaimTypes.Role, roleName));

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Issuer = "https://localhost:44305",
                Audience = "https://localhost:44305",
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Today.AddDays(7),
                SigningCredentials = credentials,
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
