using AutoMapper;
using EventManager.Common.Models;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Models.Event;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventPictureRepository _eventPictureRepository;
        private readonly IJwtService _jwtService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository,
            IEventPictureRepository eventPictureRepository,
            IJwtService jwtService,
            ICloudinaryService cloudinaryService,
            IMapper mapper)
        {
            _eventPictureRepository = eventPictureRepository;
            _jwtService = jwtService;
            _eventRepository = eventRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public async Task CreateEventAsync(CreateEventServiceModel createEventServiceModel)
        {
            var newEvent = _mapper.Map<Event>(createEventServiceModel);

            await _eventRepository.AddAsync(newEvent);
        }
    }
}
