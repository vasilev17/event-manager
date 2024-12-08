using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Data.Models;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;
using EventManager.Services.Models.User;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
using EventManager.Web.Models.Event;
using EventManager.Web.Models.Picture;
using EventManager.Web.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManager.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;


        public EventController(IEventServiceFactory eventServiceFactory, IMapper mapper, IJwtService jwtService)
        {
            _eventService = eventServiceFactory.CreateEventService();
            _mapper = mapper;
            _jwtService = jwtService;
        }

        /// <summary>
        /// End point for creating a new event in the platform
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <param name="newEventModel">The model with the data for the new event</param>
        /// <param name="pictureModel">Model with the file data</param>
        /// <param name="authorization">JWT authorization token</param>
        /// <returns>A JWT token for future authentication</returns>
        [HttpPost("CreateEvent/{userId}")]
        [Authorize(Roles = RoleConstants.Creators)]
        public async Task<IActionResult> CreateEvent(Guid userId, [FromForm] CreateEventWebModel newEventModel, [FromForm] UploadPictureWebModel pictureModel, [FromHeader] string authorization)
        {
            var tokenResult = _jwtService.ValidateJwtToken(userId, authorization);

            if (!tokenResult)
                return Unauthorized(ExceptionConstants.Unauthorized);

            EventPictureServiceModel eventPictureServiceModel = null;

            if (pictureModel.Picture != null)
            {
                var ms = new MemoryStream();
                pictureModel.Picture.CopyTo(ms);

                eventPictureServiceModel = new EventPictureServiceModel
                {
                    Picture = new UploadPictureServiceModel
                    {
                        FileName = pictureModel.Picture.FileName,
                        Stream = ms
                    },
                };

            }

            var newEvent = _mapper.Map<CreateEventServiceModel>(newEventModel);
            newEvent.UserId = userId;

            await _eventService.CreateEventAsync(newEvent, eventPictureServiceModel);

            return Ok();
        }



    }
}
