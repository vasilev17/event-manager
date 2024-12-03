using EventManager.Data.Models.Picture;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IProfilePictureRepository : IBaseRepository<ProfilePicture>
    {
        /// <summary>
        /// Gets the profile picture for a user
        /// </summary>
        /// <param name="userId">Id of the user</param>
        /// <returns>The profile picture or null</returns>
        Task<ProfilePicture> GetByUserIdAsync(Guid userId);  
    }
}
