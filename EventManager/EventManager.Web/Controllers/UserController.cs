using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}
