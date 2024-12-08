using EventManager.Common.Constants;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;
using EventManager.Services.Services.Interfaces;

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

        public Task DeleteEventAsync(Guid eventID)
        {
            return _parent.DeleteEventAsync(eventID);
        }

    }
}
