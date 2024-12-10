using EventManager.Common.Constants;
using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>True if the event is deleted correctly</returns>
        Task<bool> DeleteAsync(Guid eventId);

        /// <summary>
        /// Gets all events from the database
        /// </summary>
        /// <returns>List containing all events</returns>
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

    }
}
