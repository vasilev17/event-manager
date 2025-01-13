using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using EventManager.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VerificationRequestController : Controller
    {
        private readonly IVerificationRequestService _verificationRequestService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public VerificationRequestController(IVerificationRequestServiceFactory verificationRequestServiceFactory, IMapper mapper, IJwtService jwtService)
        {
            _verificationRequestService = verificationRequestServiceFactory.Create();
            _mapper = mapper;
            _jwtService = jwtService;
        }

        /// <summary>
        /// An endpoint for organizers to request to be verified
        /// </summary>
        /// <param name="authorization">The JWT token of the organizer</param>
        /// <returns></returns>
        [HttpPost("RequestVerification")]
        [Authorize(Roles = RoleConstants.Organizer)]
        public async Task<IActionResult> RequestVerification([FromBody] VerificationRequestWebModel model, [FromHeader] string authorization)
        {
            var serviceModel = _mapper.Map<VerificationRequestServiceModel>(model);
            serviceModel.OrganizerId = _jwtService.GetId(authorization);

            await _verificationRequestService.RequestVerification(serviceModel);

            return Ok();
        }

        /// <summary>
        /// Returns all created requests for verification
        /// </summary>
        /// <returns>The list with requests</returns>
        [HttpGet("Requests")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> Requests()
        {
            return Ok(await _verificationRequestService.GetAllActiveRequestsAsync());
        }

        /// <summary>
        /// Compleates the request
        /// </summary>
        /// <param name="id">id of the request</param>
        [HttpPut("CompleateRequest/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> CompleateRequest(Guid id)
        {
            await _verificationRequestService.CompleateRequestAsync(id);

            return Ok();
        }

        /// <summary>
        /// Deletes a request
        /// </summary>
        /// <param name="id">Id of the request</param>
        [HttpDelete("DeleteRequest/{id}")]
        [Authorize(Roles = RoleConstants.Admin)]
        public async Task<IActionResult> DeleteRequest(Guid id)
        {
            await _verificationRequestService.DeleteRequestAsync(id);

            return Ok();
        }
    }
}
