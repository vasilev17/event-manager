using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Common.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Models.Picture;
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

        public async Task DeleteEventAsync(Guid eventId, Guid userId, bool isAdmin)
        {
            var result = await _eventRepository.DeleteAsync(eventId, userId, isAdmin);

            if (!result)
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToDelete, "event"));

        }

        public async Task<string> BookTicketAsync(Guid ticketId, Guid userId)
        {
            var result = await _eventRepository.BookTicketAsync(ticketId, userId);

            if (result == string.Empty)
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToCreate, "booking"));

            return result;
        }

        public async Task CreateEventTicketAsync(EventTicketServiceModel ticketModel, Guid creatorId, bool isAdmin)
        {
            var newTicket = _mapper.Map<Ticket>(ticketModel);

            await _eventRepository.AddTicketAsync(newTicket, creatorId, isAdmin);
        }

        public async Task<List<EventGridViewDTO>> GetFilteredEventsAsync(EventFilterServiceModel filter, PaginationServiceModel? paginationModel)
        {
            var pagination = _mapper.Map<Pagination>(paginationModel);
            List<Event> events;


            //Check if pagination is passed
            if (pagination.PageNumber == null && pagination.PageSize == null)
                events = await _eventRepository.GetAllEventsAsync();

            else if ((pagination.PageNumber == null && pagination.PageSize != null) || (pagination.PageNumber != null && pagination.PageSize == null))
                throw new InvalidRequestParametersException(ExceptionConstants.InvalidPaginationInput);
           
            else
                events = await _eventRepository.GetAllEventsAsync(pagination);


            List<EventDTO> eventDTOs = _mapper.Map<List<EventDTO>>(events);

            if (!string.IsNullOrEmpty(filter.Name))
            {
                eventDTOs = eventDTOs.Where(e => e.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (filter.MinPrice.HasValue)
            {
                eventDTOs = eventDTOs.Where(e => e.MaxPrice >= filter.MinPrice.Value).ToList();
            }

            if (filter.MaxPrice.HasValue)
            {
                eventDTOs = eventDTOs.Where(e => e.MinPrice <= filter.MaxPrice.Value || e.MinPrice == null).ToList();
            }

            if (filter.StartDateTime.HasValue)
            {
                eventDTOs = eventDTOs.Where(e => e.StartDateTime >= filter.StartDateTime.Value).ToList();
            }

            if (filter.EndDateTime.HasValue)
            {
                eventDTOs = eventDTOs.Where(e => e.EndDateTime <= filter.EndDateTime.Value).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Address))
            {
                eventDTOs = eventDTOs.Where(e => e.Address.Contains(filter.Address, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (!string.IsNullOrEmpty(filter.Username))
            {
                eventDTOs = eventDTOs.Where(e => e.CreatorUsername == filter.Username).ToList();
            }

            if (filter.EventTypes != null && filter.EventTypes.Any())
            {
                var eventTypeNames = filter.EventTypes.Select(et => et.ToString()).ToHashSet();
                eventDTOs = eventDTOs.Where(e => e.EventTypeNames.Overlaps(eventTypeNames)).ToList();
            }

            List<EventGridViewDTO> eventViewDTOs = _mapper.Map<List<EventGridViewDTO>>(eventDTOs);

            return eventViewDTOs;
        }

        public async Task<EventDTO> GetEventAsync(Guid eventId)
        {
            var searchedEvent = await _eventRepository.GetSingleEventAsync(eventId);

            var eventDTO = _mapper.Map<EventDTO>(searchedEvent);

            return eventDTO;
        }

        public async Task<float> RateEventAsync(RateEventServiceModel ratingModel)
        {
            var rating = _mapper.Map<Rating>(ratingModel);

            var savedRating = await _eventRepository.AddEventRatingAsync(rating);

            if (!savedRating)
            {
                throw new DatabaseException(string.Format(ExceptionConstants.FailedToCreate, "rating"));
            }

            return await _eventRepository.UpdateEventAverageRatingAsync(rating.EventId);

        }

        public async Task ToggleEventAttendanceAsync(Guid eventId, Guid userId)
        {
            var result = await _eventRepository.ToggleAttendanceAsync(eventId, userId);

            if (!result)
                throw new CreationDatabaseException(string.Format(ExceptionConstants.FailedToCreate, "attendance"));

        }

        //public async Task UploadEventPictureAsync(EventPictureServiceModel model)
        //{
        //    await SavePicture(model);
        //}

        //public async Task DeleteEventPictureAsync(Guid id)
        //{
        //    var picture = await _eventPictureRepository.GetByEventIdAsync(id);

        //    if (picture != null)
        //    {
        //        var pictureServiceModel = _mapper.Map<PictureServiceModel>(picture);
        //        await _cloudinaryService.DeletePictureAsync(pictureServiceModel);
        //    }

        //    await _eventPictureRepository.DeleteAsync(picture);

        //    var searchedEvent = await _eventRepository.GetByIdAsync(id);
        //    searchedEvent.EventPicture = new EventPicture
        //    {
        //        Url = PictureConstants.DefaultEventPicture
        //    };

        //    await _eventRepository.EditAsync(id, searchedEvent);
        //}

        //private async Task SavePicture(EventPictureServiceModel model)
        //{
        //    model.Picture.Stream.Seek(0, SeekOrigin.Begin);
        //    var cloudinaryResult = await _cloudinaryService.UploadPictureAsync(model.Picture);

        //    var eventPicture = new EventPicture
        //    {
        //        PublicId = cloudinaryResult.PublicId,
        //        ResourceType = cloudinaryResult.ResourceType,
        //        Url = cloudinaryResult.Url.AbsoluteUri
        //    };
        //    var searchedEvent = await _eventRepository.GetByIdAsync(model.EventId);

        //    searchedEvent.EventPicture = eventPicture;
        //    eventPicture.Event = searchedEvent;
        //    eventPicture.EventId = searchedEvent.Id;

        //    var pictureIsSaved = await _eventPictureRepository.AddAsync(eventPicture);

        //    if (!pictureIsSaved)
        //        throw new DatabaseException(string.Format(ExceptionConstants.FailedToUpload, "event picture"));

        //    var eventResult = await _eventRepository.EditAsync(searchedEvent.Id, searchedEvent);

        //    if (!eventResult)
        //        throw new DatabaseException(string.Format(ExceptionConstants.FailedToUpdate, "event"));
        //}

    }
}
