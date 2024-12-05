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

            using var transaction = await DbContext.Database.BeginTransactionAsync();

            try
            {

                // Validate if the event already exists
                var existingEvent = await DbContext.Events.FirstOrDefaultAsync(e => e.Name == entity.Name && e.StartDateTime == entity.StartDateTime);
                if (existingEvent != null)
                {
                    throw new CreationDatabaseException(string.Format(ExceptionConstants.AlreadyExists, "event"));
                }

                // Add the event to the database without committing
                DbContext.Events.Add(entity);
                var result = await DbContext.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "event"));
                }

                // Commit the transaction
                await transaction.CommitAsync();

                return true;

            }
            catch
            {

                await transaction.RollbackAsync();
                throw;
            }


        }



    }
}
