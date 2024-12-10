using EventManager.Common.Constants;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

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
                throw new CreationDatabaseException(string.Format(ExceptionConstants.FailedToCreate, "event"));

            }

            return true;
        }

        public override async Task<Event> GetByIdAsync(Guid id)
        {
            var searchedEvent = await base.GetByIdAsync(id);

            if (searchedEvent == null)
                throw new DatabaseException(string.Format(ExceptionConstants.NotFound, "event"));


            return searchedEvent;
        }

        public async Task<List<Event>> GetAllEventsAsync()
        {
            var events = await DbContext.Events
                .Include(e => e.User)
                .Include(e => e.Types)
                .ToListAsync();

            return events;
        }

        public async Task<bool> AddEventRatingAsync(Rating entity)
        {

            //Check if this is an event (and not an activity) and it has started.

            var eventToRate = await GetByIdAsync(entity.EventId);

            if (eventToRate == null)
            {
                throw new DatabaseException(string.Format(ExceptionConstants.NotFound, "event"));

            }


            if (!eventToRate.IsActivity && DateTime.Now < eventToRate.StartDateTime)
            {
                throw new ArgumentException(ExceptionConstants.InvalidRatingTime);
            }

            var existingRating = await DbContext.Ratings
                .FirstOrDefaultAsync(e => e.UserId == entity.UserId && e.EventId == entity.EventId);

            if (existingRating != null)
            {
                throw new CreationDatabaseException(string.Format(ExceptionConstants.AlreadyExists, "rating"));
            }

            // Add the new Rating to the context
            DbContext.Ratings.Add(entity);
            var result = await DbContext.SaveChangesAsync();

            if (result <= 0)
            {
                throw new CreationDatabaseException(string.Format(ExceptionConstants.FailedToCreate, "rating"));
            }

            return true;
        }

        public async Task<float> UpdateEventAverageRatingAsync(Guid eventId)
        {

            var eventEntity = await DbContext.Events
                                    .Include(e => e.Ratings)
                                    .FirstOrDefaultAsync(e => e.Id == eventId);

            if (eventEntity == null)
            {
                throw new DatabaseException(string.Format(ExceptionConstants.NotFound, "event"));
            }

            float avgRating = 0f;

            if (eventEntity.Ratings.Any())
            {
                avgRating = eventEntity.Ratings.Average(r => r.RatingValue);
            }

            eventEntity.AverageRating = avgRating;
            await DbContext.SaveChangesAsync();

            return avgRating;
        }

        public async Task<bool> ToggleAttendanceAsync(Guid eventId, Guid userId)
        {

            //Check if the event exists
            var existingEvent = await GetByIdAsync(eventId);

            // Validate if the event already exists
            var existingAttendance = await DbContext.Attendances
                .FirstOrDefaultAsync(e => e.UserId == userId && e.EventId == eventId);

            if (existingAttendance != null)
            {
                DbContext.Attendances.Remove(existingAttendance);
                var result = await DbContext.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new DatabaseException(string.Format(ExceptionConstants.FailedToDelete, "attendance"));
                }

            }
            else
            {

                // Add the new Attendance to the context
                DbContext.Attendances.Add(new Attendance { UserId = userId, EventId = eventId });
                var result = await DbContext.SaveChangesAsync();

                if (result <= 0)
                {
                    throw new CreationDatabaseException(string.Format(ExceptionConstants.FailedToCreate, "attendance"));
                }
            }

            return true;

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
                throw new DatabaseException(ExceptionConstants.NotFound + "event");
            }

            return await base.DeleteAsync(eventInDb);

        }

    }
}
