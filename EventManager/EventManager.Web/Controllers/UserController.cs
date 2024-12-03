using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Models.Picture;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using EventManager.Web.Models.Picture;
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
        private readonly IJwtService _jwtService;

        public UserController(IUserServiceFactory userServiceFactory, IMapper mapper, IJwtService jwtService)
        {
            _userService = userServiceFactory.CreateUserService();
            _mapper = mapper;
            _jwtService = jwtService;
        }

        /// <summary>
        /// End point for registering new users in the platform
        /// </summary>
        /// <param name="registerWebModel">The model with the data for the new user</param>
        /// <returns>A JWT token for future authentication</returns>
        [HttpPost("Register")]
        [AllowAnonymous]
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
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginWebModel loginWebModel)
        {
            var loginServiceModel = _mapper.Map<LoginServiceModel>(loginWebModel);

            return Ok(await _userService.LoginAsync(loginServiceModel));
        }

        /// <summary>
        /// Request a token (via mail) to reset a password
        /// </summary>
        /// <param name="resetPasswordWebModel">Model containing the data for the user requesting the token</param>
        /// <returns>Ok result after sending the email with the token</returns>
        [HttpGet("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordWebModel resetPasswordWebModel)
        {
            var resetPasswordServiceModel = _mapper.Map<ResetPasswordServiceModel>(resetPasswordWebModel);

            await _userService.SendResendPasswordAsync(resetPasswordServiceModel);

            return Ok();
        }

        /// <summary>
        /// Request a token (via local file) to reset a password
        /// </summary>
        /// <param name="resetPasswordWebModel">Model containing the data for the user requesting the token</param>
        /// <returns>Ok result after creating the file with the token</returns>
        [HttpGet("ResetPasswordLocal")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordLocal([FromBody] ResetPasswordWebModel resetPasswordWebModel)
        {
            var resetPasswordServiceModel = _mapper.Map<ResetPasswordServiceModel>(resetPasswordWebModel);

            await _userService.ResendPasswordLocalAsync(resetPasswordServiceModel);

            return Ok();
        }

        /// <summary>
        /// Resets the passowrd of a user with a provided token
        /// </summary>
        /// <param name="resetPasswordWebModel">Model with the data for the password reset</param>
        /// <returns>Ok result after the passowrd was changed</returns>
        [HttpPost("ResetPassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordTokenWebModel resetPasswordWebModel)
        {
            var tokenServiceModel = _mapper.Map<ResetPasswordTokenServiceModel>(resetPasswordWebModel);

            await _userService.ResetPasswordAsync(tokenServiceModel);

            return Ok();
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">Id of the user to be deleted</param>
        /// <param name="authorization">The token of the user</param>
        /// <returns>Ok result after deleting the user</returns>
        [HttpDelete("Delete/{id}")]
        [Authorize()]
        public async Task<IActionResult> Delete(Guid id, [FromHeader] string authorization)
        {
            var tokenResult = _jwtService.ValidateJwtToken(id, authorization);

            if(!tokenResult)
                return Unauthorized(ExceptionConstants.Unauthorized);

            await _userService.DeleteUserAsync(id);

            return Ok();
        }

        /// <summary>
        /// Endpoint for uptating a user
        /// </summary>
        /// <param name="id">Users id</param>
        /// <param name="updateModel">The model. It's params can be null, in that case old once will be kept</param>
        /// <param name="authorization">User's token</param>
        /// <returns>Ok result</returns>
        [HttpPut("Update/{id}")]
        [Authorize()]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserWebModel updateModel, [FromHeader] string authorization)
        {
            var tokenResult = _jwtService.ValidateJwtToken(id, authorization);

            if (!tokenResult)
                return Unauthorized(ExceptionConstants.Unauthorized);

            var serviceModel = _mapper.Map<UpdateUserServiceModel>(updateModel);

            await _userService.UpdateUserAsync(id, serviceModel);

            return Ok();
        }

        /// <summary>
        /// Uploads a profile picture
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <param name="model">Model with the file data</param>
        /// <param name="authorization">JWT authorizaation token</param>
        /// <returns></returns>
        [HttpPut("UploadPicture/{id}")]
        [Authorize()]
        public async Task<IActionResult> UpdateProfilePicture(Guid id, [FromForm] UploadProfilePictureWebModel model, [FromHeader] string authorization)
        {
            var tokenResult = _jwtService.ValidateJwtToken(id, authorization);

            if (!tokenResult)
                return Unauthorized(ExceptionConstants.Unauthorized);

            var ms = new MemoryStream();
            model.Picture.CopyTo(ms);
            var profilePictureServiceModel = new ProfilePictureServiceModel
            {
                Picture = new UploadPictureServiceModel
                {
                    FileName = model.Picture.FileName,
                    Stream = ms
                },
                UserId = id
            };

            await _userService.UploadProfilePictureAsync(profilePictureServiceModel);
            return Ok();
        }

        [HttpDelete("DeleteProfilePicture/{id}")]
        [Authorize()]
        public async Task<IActionResult> DeleteProfilePicture(Guid id, [FromHeader] string authorization)
        {
            var tokenResult = _jwtService.ValidateJwtToken(id, authorization);

            if (!tokenResult)
                return Unauthorized(ExceptionConstants.Unauthorized);

            await _userService.DeleteProfilePictureAsync(id);

            return Ok();
        }
    }
}
