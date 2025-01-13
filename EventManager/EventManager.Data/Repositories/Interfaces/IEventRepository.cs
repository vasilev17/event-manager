using EventManager.Common.Models;
using EventManager.Data.Models;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IEventRepository : IBaseRepository<Event>
    {
        /// <summary>
        /// Adds an event to the database
        /// </summary>
        /// <param name="entity">The entity with the event data</param>
        /// <returns>True if the user is added correctly</returns>
        Task<bool> AddAsync(Event entity);

        /// <summary>
        /// Deletes an event from the database
        /// </summary>
        /// <param name="eventId">Id of the event to be deleted</param>
        /// <param name="userId">Id of the user deleting the event</param>
        /// <param name="isAdmin">Info whether the caller is admin</param>
        /// <returns>True if the event is deleted correctly</returns>
        Task<bool> DeleteAsync(Guid eventId, Guid userId, bool isAdmin);

        /// <summary>
        /// Gets an event from the database
        /// </summary>
        /// <param name="eventId">Id of the searched event</param>
        /// <returns>The searched Event</returns>
        Task<Event> GetSingleEventAsync(Guid eventId);

        /// <summary>
        /// Gets all events from the database with pagination
        /// </summary>
        /// <param name="pagination">Pagination data for the retrieval</param>
        /// <returns>List of paginated Events</returns>
        Task<List<Event>> GetAllEventsAsync(Pagination pagination);

        /// <summary>
        /// Overloaded method for getting all events in the DB (without pagination)
        /// </summary>
        /// <returns>List of all Events</returns>
        Task<List<Event>> GetAllEventsAsync();

        /// <summary>
        /// Adds a rating to the database
        /// </summary>
        /// <param name="entity">The entity with the rating data</param>
        /// <returns>True if the rating is added correctly</returns>
        Task<bool> AddEventRatingAsync(Rating entity);

        /// <summary>
        /// Updates the average rating of an event in the database
        /// </summary>
        /// <param name="eventId">Id of the event which average rating to change</param>
        /// <returns>The new average rating</returns>
        Task<float> UpdateEventAverageRatingAsync(Guid eventId);

        /// <summary>
        /// Adds a new attendance in the database or deletes an existing one
        /// </summary>
        /// <param name="eventId">Id of the attended event</param>
        /// <param name="userId">Id of the attending user</param>
        /// <returns>True if the attendance is added/removed correctly</returns>
        Task<bool> ToggleAttendanceAsync(Guid eventId, Guid userId);

        /// <summary>
        /// Assigns a ticket in the database to a user (booking it)
        /// </summary>
        /// <param name="ticketId">Id of the ticket to be marked as booked</param>
        /// <param name="userId">Id of the user booking the event ticket</param>
        /// <returns>The barcode of the booked ticket</returns>
        Task<string> BookTicketAsync(Guid ticketId, Guid userId);

        /// <summary>
        /// Checks and sets min and max price for an event based on the tickets available
        /// </summary>
        /// <param name="ticketId">Id of the event</param>
        /// <returns>True if the prices are updated correctly</returns>
        Task<bool> UpdateEventPricesAsync(Guid eventId);

        /// <summary>
        /// Creates a new ticket in the database
        /// </summary>
        /// <param name="newTicket">Model containg the new ticket data</param>
        /// <param name="creatorId">Id of the user trying to create the ticket</param>
        /// <param name="isAdmin">Info whether the caller is admin</param>
        /// <returns>True if the ticket is created correctly</returns>
        Task<bool> AddTicketAsync(Ticket newTicket, Guid creatorId, bool isAdmin);

        /// <summary>
        /// Gets all events by organizer
        /// </summary>
        /// <param name="id"></param>
        Task<IEnumerable<Event>> GetAllEventsByOrganizerAsync(Guid id);

    }
}
