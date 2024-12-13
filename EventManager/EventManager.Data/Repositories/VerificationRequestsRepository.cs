using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public class VerificationRequestsRepository : BaseRepository<VerificationRequest>, IVerificationRequestsRepository
    {
        public VerificationRequestsRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<VerificationRequest> GetAllActiveRequests()
        {
            return DbContext
                .Set<VerificationRequest>()
                .Include(x => x.Organizer)
                .Where(x => x.IsCompleated == false)
                .ToList();
        }

        public override Task<VerificationRequest> GetByIdAsync(Guid id)
        {
            return DbContext
                .Set<VerificationRequest>()
                .Include(x => x.Organizer)
                .FirstAsync(x => x.Id == id);
        }
    }
}
