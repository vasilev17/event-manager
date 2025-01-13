using AutoMapper;
using EventManager.Data.Repositories;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Decorators.Event;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Factories
{
    public class EventServiceFactory : IEventServiceFactory
    {
        private readonly IEventRepository _eventRepository;
        private readonly IEventPictureRepository _eventPictureRepository;
        private readonly IJwtService _jwtService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;

        public EventServiceFactory(
            IEventRepository eventRepository,
            IEventPictureRepository eventPictureRepository,
            ICloudinaryService cloudinaryService,
            IJwtService jwtService,
            IMapper mapper)
        {
            _eventRepository = eventRepository;
            _eventPictureRepository = eventPictureRepository;
            _jwtService = jwtService;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
        }

        public IEventService Create()
        {
            var coreService = new EventService(_eventRepository, _eventPictureRepository, _jwtService, _cloudinaryService, _mapper);
            return new ValidationEventDecorator(coreService);
        }
    }
}
