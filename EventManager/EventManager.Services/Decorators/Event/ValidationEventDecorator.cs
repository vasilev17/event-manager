using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Data.Models;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;
using EventManager.Services.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace EventManager.Services.Decorators.Event
{
    public class ValidationEventDecorator : IEventService
    {
        private readonly IEventService _parent;


        public ValidationEventDecorator(IEventService parent)
        {
            _parent = parent;
        }

        public Task CreateEventAsync(CreateEventServiceModel newEvent, EventPictureServiceModel pictureModel)
        {
            ValidateCreateEventModel(newEvent);

            return _parent.CreateEventAsync(newEvent, pictureModel);
        }

        public void ValidateCreateEventModel(CreateEventServiceModel newEvent)
        {
            if (newEvent.Name.Length < 3 || newEvent.Address.Length < 3)
            {
                throw new ArgumentException(ExceptionConstants.InvalidEventDataInput);
            }

            if ((newEvent.StartDateTime != null || newEvent.EndDateTime != null) && newEvent.IsActivity == true)
            {
                throw new ArgumentException(ExceptionConstants.InvalidActivityDateTimes);
            }

            if ((newEvent.StartDateTime == null || newEvent.EndDateTime == null) && newEvent.IsActivity == false)
            {
                throw new ArgumentException(ExceptionConstants.InvalidEventDateTimes);
            }
        }

        public Task DeleteEventAsync(Guid eventId)
        {
            return _parent.DeleteEventAsync(eventId);
        }

        public Task<List<EventDTO>> GetFilteredEventsAsync(EventFilterServiceModel filter)
        {
            ValidateEventFilterModel(filter);
            return _parent.GetFilteredEventsAsync(filter);
        }

        public void ValidateEventFilterModel(EventFilterServiceModel filter)
        {
            if (filter.MinPrice < 0 || filter.MaxPrice < 0)
            {
                throw new ArgumentException(ExceptionConstants.InvalidEventDataInput);
            }
        }

        public Task<Data.Models.Event> GetEventAsync(Guid eventId)
        {
            return _parent.GetEventAsync(eventId);
        }

        public Task<float> RateEventAsync(RateEventServiceModel ratingModel)
        {
            ValidateRateEventModel(ratingModel);
            return _parent.RateEventAsync(ratingModel);

        }

        public void ValidateRateEventModel(RateEventServiceModel ratingModel)
        {
            if (ratingModel.RatingValue < 0 || ratingModel.RatingValue > 5)
            {
                throw new ArgumentException(ExceptionConstants.InvalidRatingValue);
            }

            if (ratingModel.RatingValue % 0.5 != 0)
            {
                throw new ArgumentException(ExceptionConstants.InvalidRatingValueStep);
            }
        }

        public Task ToggleEventAttendanceAsync(Guid eventId, Guid userId)
        {
            return _parent.ToggleEventAttendanceAsync(eventId, userId);
        }

        public Task BookTicketAsync(Guid ticketId, Guid userId)
        {
            return _parent.BookTicketAsync(ticketId, userId);
        }

        public Task CreateEventTicketAsync(EventTicketServiceModel ticketModel)
        {
            ValidateTicketModel(ticketModel);
            return _parent.CreateEventTicketAsync(ticketModel);
        }

        public void ValidateTicketModel(EventTicketServiceModel ticketModel)
        {
            if (ticketModel.Price < 0)
            {
                throw new ArgumentException(ExceptionConstants.InvalidEventDataInput);
            }

            if (ticketModel.Type.Length < 2)
            {
                throw new ArgumentException(ExceptionConstants.InvalidEventDataInput);
            }
        }
    }
}
