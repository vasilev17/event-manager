using EventManager.Services.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace EventManager.Services.Services
{
    public class JwtService : IJwtService
    {
        private readonly byte[] _signingKey;
        private readonly TimeSpan _tokenDuration;
        private readonly string _issuer;
        private readonly string _audience;

        private const string Id = "ID";
        private const string User = "Username";

        public JwtService(byte[] signingKey, TimeSpan tokenDuration, string issuer, string audience)
        {
            _signingKey = signingKey;
            _tokenDuration = tokenDuration;
            _issuer = issuer; 
            _audience = audience;
        }

        public string GenerateJwtToken(Guid userId, string username, IList<string> roleNames)
        {
            var securityKey = new SymmetricSecurityKey(_signingKey);
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            HashSet<Claim> claims = new()
            {
                new Claim(Id, $"{userId}"),
                new Claim(User, username)
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

        public bool ValidateJwtToken(Guid userId, string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = GetValidationParameters();

            JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);

            IPrincipal principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            if (principal.Identity == null || !principal.Identity.IsAuthenticated)
                return false;
            else if (jwtToken.Claims.FirstOrDefault(x => x.Type == Id).Value != userId.ToString())
                return false;
            else
                return true;
        }

        private TokenValidationParameters GetValidationParameters()
        {
            return new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(this._signingKey)
            };
        }
    }
}
