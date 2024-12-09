using EventManager.Common.Constants;
using EventManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IEventRepository: IBaseRepository<Event>
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
        /// <returns>True if the rating is added correctly</returns>
        Task<bool> AddEventRatingAsync(Rating entity);

        /// <summary>
        /// Updates the average rating of an event
        /// </summary>
        /// <returns>The new average rating</returns>
        Task<float> UpdateEventAverageRatingAsync(Guid eventId);

    }
}
