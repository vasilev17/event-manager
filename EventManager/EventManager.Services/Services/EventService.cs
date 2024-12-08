using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Models.Picture;
using EventManager.Data.Repositories;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;
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

        public async Task CreateEventAsync(CreateEventServiceModel createEventServiceModel, EventPictureServiceModel pictureModel)
        {
            var newEvent = _mapper.Map<Event>(createEventServiceModel);

            if (pictureModel != null)
            {
                pictureModel.EventId = newEvent.Id;
                pictureModel.Picture.Stream.Seek(0, SeekOrigin.Begin);
                var cloudinaryResult = await _cloudinaryService.UploadPictureAsync(pictureModel.Picture);

                var eventPicture = new EventPicture
                {
                    PublicId = cloudinaryResult.PublicId,
                    ResourceType = cloudinaryResult.ResourceType,
                    Url = cloudinaryResult.Url.AbsoluteUri
                };

                newEvent.EventPicture = eventPicture;
            }


            await _eventRepository.AddAsync(newEvent);
        }

        public async Task DeleteEventAsync(Guid eventId)
        {
            var result = await _eventRepository.DeleteAsync(eventId);

            if (!result)
                throw new DatabaseException(ExceptionConstants.FailedToDeleteEvent);
        }

        public async Task UploadEventPictureAsync(EventPictureServiceModel model)
        {
            await SavePicture(model);
        }

        public async Task DeleteEventPictureAsync(Guid id)
        {
            var picture = await _eventPictureRepository.GetByEventIdAsync(id);

            if (picture != null)
            {
                var pictureServiceModel = _mapper.Map<PictureServiceModel>(picture);
                await _cloudinaryService.DeletePictureAsync(pictureServiceModel);
            }

            await _eventPictureRepository.DeleteAsync(picture);

            var searchedEvent = await _eventRepository.GetByIdAsync(id);
            searchedEvent.EventPicture = new EventPicture
            {
                Url = PictureConstants.DefaultEventPicture
            };

            await _eventRepository.EditAsync(id, searchedEvent);
        }

        private async Task SavePicture(EventPictureServiceModel model)
        {
            model.Picture.Stream.Seek(0, SeekOrigin.Begin);
            var cloudinaryResult = await _cloudinaryService.UploadPictureAsync(model.Picture);

            var eventPicture = new EventPicture
            {
                PublicId = cloudinaryResult.PublicId,
                ResourceType = cloudinaryResult.ResourceType,
                Url = cloudinaryResult.Url.AbsoluteUri
            };
            var searchedEvent = await _eventRepository.GetByIdAsync(model.EventId);

            searchedEvent.EventPicture = eventPicture;
            eventPicture.Event = searchedEvent;
            eventPicture.EventId = searchedEvent.Id;

            var pictureIsSaved = await _eventPictureRepository.AddAsync(eventPicture);
            if (!pictureIsSaved)
                throw new DatabaseException(ExceptionConstants.FailedToUploadEventPicture);

            var eventResult = await _eventRepository.EditAsync(searchedEvent.Id, searchedEvent);
            if (!eventResult)
                throw new DatabaseException(ExceptionConstants.FailedToUpdateEvent);
        }

    }
}
