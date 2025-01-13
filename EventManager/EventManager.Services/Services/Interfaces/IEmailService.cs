using EventManager.Services.Models.User;

namespace EventManager.Services.Services.Interfaces
{
    public interface IEmailService
    {
        /// <summary>
        /// Sends an reset password email
        /// </summary>
        /// <param name="emailMode">The model carying the information about the email that needs to be send</param>
        /// <returns>Wheather the operation was a success</returns>
        Task<bool> SendResetPasswordMailAsync(UserPasswordResetServiceModel emailMode);    
    }
}
