using EventManager.Common.Models;
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
    }
}
