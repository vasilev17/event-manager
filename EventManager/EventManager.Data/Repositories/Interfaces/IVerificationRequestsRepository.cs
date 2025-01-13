using EventManager.Data.Models;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IVerificationRequestsRepository : IBaseRepository<VerificationRequest>
    {
        /// <summary>
        /// Gets all verification requests
        /// </summary>
        /// <returns>A list</returns>
        IEnumerable<VerificationRequest> GetAllActiveRequests();
    }
}
