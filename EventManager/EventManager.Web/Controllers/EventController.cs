using AutoMapper;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
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



    }
}
