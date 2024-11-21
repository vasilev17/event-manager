using EventManager.Services.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManager.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly byte[] _signingKey;
        private readonly TimeSpan _tokenDuration;
        private readonly string _issuer;
        private readonly string _audience;

        public JwtService(byte[] signingKey, TimeSpan tokenDuration, string issuer, string audience)
        {
            _signingKey = signingKey;
            _tokenDuration = tokenDuration;
            _issuer = issuer; 
            _audience = audience;
        }

        public string GenerateJwtToken(Guid userId, string username, List<string> roleNames)
        {
            var securityKey = new SymmetricSecurityKey(_signingKey);
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
                Issuer = _issuer,
                Audience = _audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(_tokenDuration),
                SigningCredentials = credentials,
            };

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityToken token = tokenHandler.CreateToken(securityTokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
