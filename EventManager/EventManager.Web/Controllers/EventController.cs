using AutoMapper;
using EventManager.Data.Models;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.User;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
using EventManager.Web.Models.Event;
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
        /// <param name="registerWebModel">The model with the data for the new event</param>
        /// <returns>A JWT token for future authentication</returns>
        [HttpPost("CreateEvent")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateEvent([FromBody]CreateEventWebModel newEventModel)
        {
            var newEvent = _mapper.Map<CreateEventServiceModel>(newEventModel);

            await _eventService.CreateEventAsync(newEvent);

            return Ok();
        }



    }
}
