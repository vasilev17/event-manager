﻿using EventManager.Common.Constants;
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

    }
}
