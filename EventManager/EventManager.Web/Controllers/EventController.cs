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
        /// <param name="newEventModel">The model with the data for the new event</param>
        /// <param name="pictureModel">Model with the file data</param>
        /// <param name="authorization">JWT authorization token</param>
        /// <returns>Action result status</returns>
        [HttpPost("CreateEvent")]
        [Authorize(Roles = RoleConstants.Creators)]
        public async Task<IActionResult> CreateEvent([FromForm] CreateEventWebModel newEventModel, [FromForm] UploadPictureWebModel pictureModel, [FromHeader] string authorization)
        {

            if (!User.IsInRole(Roles.Admin.ToString()) && newEventModel.IsThirdParty == true)
            {
                return Unauthorized(ExceptionConstants.UnauthorizedThirdPartyCreation);
            }

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
            newEvent.Id = _jwtService.GetId(authorization);

            await _eventService.CreateEventAsync(newEvent, eventPictureServiceModel);

            return Ok();
        }


        /// <summary>
        /// End point for creating a new event in the platform
        /// </summary>
        /// <param name="eventId">Id of the event to be deleted</param>
        /// <param name="userId">The id of the user deleting the event</param>
        /// <param name="authorization">JWT authorization token</param>
        /// <returns>Action result status</returns>
        [HttpDelete("DeleteEvent/{eventId}")]
        [Authorize(Roles = RoleConstants.Creators)]
        public async Task<IActionResult> DeleteEvent(Guid eventId, [FromHeader] string authorization)
        {
            await _eventService.DeleteEventAsync(eventId);

            return Ok();
        }

        /// <summary>
        /// Endpoint for getting events
        /// </summary>
        /// <param name="filter">Model containing filters to be applied</param>
        /// <returns>Filtered list of events</returns>
        [HttpGet("GetFilteredEvents")]
        [AllowAnonymous]
        public async Task<IActionResult> GetFilteredEvents([FromQuery] EventFilterWebModel filter)
        {
            var filterServiceModel = _mapper.Map<EventFilterServiceModel>(filter);

            return Ok(await _eventService.GetFilteredEventsAsync(filterServiceModel));
        }

        /// <summary>
        /// Endpoint for getting a single event by Id
        /// </summary>
        /// /// <param name="eventId">Id of the event to be retrieved</param>
        /// <returns>Event object</returns>
        [HttpGet("GetEvent/{eventId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetEvent(Guid eventId)
        {
            return Ok(await _eventService.GetEventAsync(eventId));
        }

        /// <summary>
        /// End point for creating a new event in the platform
        /// </summary>
        /// <param name="eventId">Id of the event to be rated</param>
        /// <param name="ratingModel">Model containing rating and user data</param>
        /// <param name="authorization">JWT authorization token</param>
        /// <returns>New Event Average Rating</returns>
        [HttpPost("RateEvent/{eventId}")]
        [Authorize()]
        public async Task<IActionResult> RateEvent(Guid eventId, [FromBody] RateEventWebModel ratingModel, [FromHeader] string authorization)
        {
            var rating = _mapper.Map<RateEventServiceModel>(ratingModel);
            rating.EventId = eventId;
            rating.UserId = _jwtService.GetId(authorization);

            return Ok(await _eventService.RateEventAsync(rating));
        }

    }
}
