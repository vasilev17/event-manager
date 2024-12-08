using EventManager.Common.Constants;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public class EventRepository : BaseRepository<Event>, IEventRepository
    {
        public EventRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<bool> AddAsync(Event entity)
        {
            // Validate if the event already exists
            var existingEvent = await DbContext.Events
                .FirstOrDefaultAsync(e => e.Name == entity.Name && e.StartDateTime == entity.StartDateTime);

            if (existingEvent != null)
            {
                throw new CreationDatabaseException(string.Format(ExceptionConstants.AlreadyExists, "event"));
            }

            // Iterate over all event types in the HashSet
            foreach (var eventType in entity.Types.ToList()) // .ToList() to avoid modifying collection while iterating
            {
                // Check if each event type already exists in the database
                var type = await DbContext.Types
                    .FirstOrDefaultAsync(et => et.Name == eventType.Name);

                if (type == null)
                {
                    // If it doesn't exist, add it to the context
                    DbContext.Types.Add(eventType);
                }
                else
                {
                    // If it exists, update the eventType to the existing one in the database
                    entity.Types.Remove(eventType);  // Remove the old one from the HashSet
                    entity.Types.Add(type);          // Add the existing one from the database
                }
            }

            // Add the new Event to the context
            DbContext.Events.Add(entity);
            var result = await DbContext.SaveChangesAsync();

            if (result <= 0)
            {
                throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "event"));
            }

            return true;
        }


        public override async Task<Event> GetByIdAsync(Guid id)
        {
            var searchedEvent = await base.GetByIdAsync(id);

            if (searchedEvent == null)
                throw new ArgumentException(ExceptionConstants.EventNotFound);

            return searchedEvent;
        }

        public override async Task<bool> EditAsync(Guid id, Event newEntity)
        {
            newEntity.Id = id;
            var result = await base.EditAsync(id, newEntity);

            return result;
        }

        public async Task<bool> DeleteAsync(Guid eventId)
        {
            var eventInDb = await GetByIdAsync(eventId);

            if (eventInDb == null)
            {
                throw new ArgumentException(ExceptionConstants.EventNotFound);
            }

            return await base.DeleteAsync(eventInDb);

        }



    }
}
