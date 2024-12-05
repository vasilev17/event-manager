using EventManager.Data.Models.Picture;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Data.Repositories
{
    public class EventPictureRepository : BaseRepository<EventPicture>, IEventPictureRepository
    {
        public EventPictureRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<EventPicture> GetByEventIdAsync(Guid eventId)
        {
            return DbContext.EventPictures
                .Include(x => x.Event)
                .FirstOrDefaultAsync(x => x.EventId == eventId);
        }
    }
}
