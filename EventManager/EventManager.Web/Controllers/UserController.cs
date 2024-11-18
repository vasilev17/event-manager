using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterWebModel registerWebModel)
        {
            var user = new User
            {
                UserName = registerWebModel.UserName,
                Email = registerWebModel.Email,
                FirstName = registerWebModel.FirstName,
                LastName = registerWebModel.LastName,
                PasswordHash = registerWebModel.Password,
                PictureURL = "Lorem ipsum"
            };

            return new CreatedResult("Register", await RegisterUser(user));
        }

        private async Task<TokenModel> RegisterUser(User user)
        {
            var userResult = await _userRepository.AddAsync(user);
            var craetedUser = await _userRepository.GetByNameAsync(user.UserName);
            return new TokenModel(GenerateJwtToken(user.Id, user.UserName, new List<string>()));
        }

        private string GenerateJwtToken(Guid userId, string username, List<string> roleNames)
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
