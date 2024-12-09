using EventManager.Data.Models.Picture;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IEventPictureRepository : IBaseRepository<EventPicture>
    {
        /// <summary>
        /// Gets the event picture for an event
        /// </summary>
        /// <param name="eventId">Id of the event</param>
        /// <returns>The event picture or null</returns>
        Task<EventPicture> GetByEventIdAsync(Guid eventId);
    }
}
