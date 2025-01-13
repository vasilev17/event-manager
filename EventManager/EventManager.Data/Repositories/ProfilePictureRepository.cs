using EventManager.Data.Models.Picture;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public class ProfilePictureRepository : BaseRepository<ProfilePicture>, IProfilePictureRepository
    {
        public ProfilePictureRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Task<ProfilePicture> GetByUserIdAsync(Guid userId)
        {
            return DbContext.ProfilePictures
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
