using EventManager.Common.Models;
using EventManager.Services.Models.Event;
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
        Task CreateEventAsync(CreateEventServiceModel createEventServiceModel);

    }
}
