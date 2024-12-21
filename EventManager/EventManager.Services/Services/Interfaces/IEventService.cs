using EventManager.Common.Models;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;

namespace EventManager.Services.Services.Interfaces
{
    public interface IEventService
    {

        /// <summary>
        /// Creates an event
        /// </summary>
        /// <param name="createEventServiceModel">Model carying event data</param>
        Task CreateEventAsync(CreateEventServiceModel createEventServiceModel, EventPictureServiceModel pictureModel);

        /// <summary>
        /// Deletes an event
        /// </summary>
        /// <param name="eventId">Id of the event to be deleted</param>
        Task DeleteEventAsync(Guid eventId);

        /// <summary>
        /// Gets events based on applied filters
        /// </summary>
        /// <param name="filter">Model carying the filters to be applied</param>
        /// <param name="paginationModel">Model carying the pagination data for the retrieval</param>
        /// <returns>List of Event Grid View DTOs of filtered events</returns>
        Task<List<EventGridViewDTO>> GetFilteredEventsAsync(EventFilterServiceModel filter, PaginationServiceModel paginationModel);

        /// <summary>
        /// Gets event based on the Id provided
        /// </summary>
        /// <param name="eventId">The id of the searched event</param>
        /// <returns>Single EventDTO object</returns>
        Task<EventDTO> GetEventAsync(Guid eventId);

        /// <summary>
        /// Creates a new event rating in the database
        /// </summary>
        /// <param name="ratingModel">Model carying the rating data</param>
        /// <returns>The new event average rating</returns>
        Task<float> RateEventAsync(RateEventServiceModel ratingModel);

        /// <summary>
        /// Marks user as attending or not attending to an event
        /// </summary>
        /// <param name="eventId">The id of the event the user is attending</param>
        /// <param name="userId">The id of the attending user </param>
        Task ToggleEventAttendanceAsync(Guid eventId, Guid userId);

        /// <summary>
        /// Makes a ticket booked by a user
        /// </summary>
        /// <param name="ticketId">Id of the ticket the user is booking</param>
        /// <param name="userId">Id of the user booking the event</param>
        Task BookTicketAsync(Guid ticketId, Guid userId);

        /// <summary>
        /// Creates a new event ticket
        /// </summary>
        /// <param name="ticketModel">Model containing the ticket data</param>
        Task CreateEventTicketAsync(EventTicketServiceModel ticketModel);



    }
}
