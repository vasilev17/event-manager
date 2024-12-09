using EventManager.Common.Models;
using EventManager.Data.Models;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.Picture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <returns>List of filtered events</returns>
        Task<List<Event>> GetFilteredEvents(EventFilterServiceModel filter);

        /// <summary>
        /// Gets event based on the Id provided
        /// </summary>
        /// <returns>Single Event object</returns>
        Task<Event> GetEvent(Guid eventId);
    }
}
