using EventManager.Common.Constants;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
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
            var existingEvent = await DbContext.Events.FirstOrDefaultAsync(e => e.Name == entity.Name && e.StartDateTime == entity.StartDateTime);
            if (existingEvent != null)
            {
                throw new CreationDatabaseException(string.Format(ExceptionConstants.AlreadyExists, "event"));
            }

            DbContext.Events.Add(entity);
            var result = await DbContext.SaveChangesAsync();

            if (result <= 0)
            {
                throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "event"));
            }

            return true;

        }



    }
}
