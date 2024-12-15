using EventManager.Services.Models.User;

namespace EventManager.Services.Services.Interfaces
{
    public interface IVerificationRequestService
    {
        /// <summary>
        /// Creates a verification request for a organizer
        /// </summary>
        /// <param name="guid">The id of the organizer</param>
        Task RequestVerification(VerificationRequestServiceModel model);

        /// <summary>
        /// Gets all verification requests
        /// </summary>
        /// <returns>A lsit</returns>
        Task<IEnumerable<VerificationRequestInfo>> GetAllActiveRequestsAsync();
        
        /// <summary>
        /// Compleates a verification request and marks the organizer as verified
        /// </summary>
        /// <param name="id">The id of the request</param>
        Task CompleateRequestAsync(Guid id);

        /// <summary>
        /// Delets a request
        /// </summary>
        /// <param name="id">Id of the request</param>
        Task DeleteRequestAsync(Guid id);
    }
}
