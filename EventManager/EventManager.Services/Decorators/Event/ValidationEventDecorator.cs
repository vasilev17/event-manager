using EventManager.Common.Constants;
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

        public Task<List<Data.Models.Event>> GetFilteredEventsAsync(EventFilterServiceModel filter)
        {
            return _parent.GetFilteredEventsAsync(filter);
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

        public Task ToggleEventAttendanceAsync(Guid eventId, Guid userId)
        {
            return _parent.ToggleEventAttendanceAsync(eventId, userId);
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
    }
}
