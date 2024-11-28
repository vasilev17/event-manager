using AutoMapper;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using EventManager.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.Controllers
{
    /// <summary>
    /// Controller containing the endpoints for handling requests about users
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserServiceFactory userServiceFactory, IMapper mapper)
        {
            _userService = userServiceFactory.CreateUserService();
            _mapper = mapper;
        }

        /// <summary>
        /// End point for registering new users in the platform
        /// </summary>
        /// <param name="registerWebModel">The model with the data for the new user</param>
        /// <returns>A JWT token for future authentication</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterWebModel registerWebModel)
        {
            var user = _mapper.Map<RegisterServiceModel>(registerWebModel);

            return new CreatedResult("Register", await _userService.RegisterAsync(user));
        }

        /// <summary>
        /// End point for logging in the platform
        /// </summary>
        /// <param name="loginWebModel">The model with the login data</param>
        /// <returns>A JWT token for future authentication</returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        //TODO: Handle ivalid login data 500
        public async Task<IActionResult> Login([FromBody] LoginWebModel loginWebModel)
        {
            var loginServiceModel = _mapper.Map<LoginServiceModel>(loginWebModel);

            return Ok(await _userService.LoginAsync(loginServiceModel));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordWebModel resetPasswordWebModel)
        {
            var resetPasswordServiceModel = _mapper.Map<ResetPasswordServiceModel>(resetPasswordWebModel);

            await _userService.SendResendPasswordAsync(resetPasswordServiceModel);

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordTokenWebModel resetPasswordWebModel)
        {
            var tokenServiceModel = _mapper.Map<ResetPasswordTokenServiceModel>(resetPasswordWebModel);

            await _userService.ResetPasswordAsync(tokenServiceModel);

            return Ok();
        }
    }
}
