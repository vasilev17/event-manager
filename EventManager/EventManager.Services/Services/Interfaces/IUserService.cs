using EventManager.Common.Models;
using EventManager.Services.Models.User;

namespace EventManager.Services.Services.Interfaces
{
    public interface IUserService
    {
        /// <summary>
        /// Logs user in the system
        /// </summary>
        /// <param name="loginServiceModel">Model carying user data</param>
        /// <returns>JWT Token for further login</returns>
        Task<TokenModel> LoginAsync(LoginServiceModel loginServiceModel);

        /// <summary>
        /// Registers user in the system
        /// </summary>
        /// <param name="loginServiceModel">Model carying user data</param>
        /// <returns>JWT Token for further login</returns>
        Task<TokenModel> RegisterAsync(RegisterServiceModel user);

        /// <summary>
        /// Generates password token for a user and sends him an email with it
        /// </summary>
        /// <param name="resetPasswordServiceModel">The model carying the data</param>
        Task SendResendPasswordAsync(ResetPasswordServiceModel resetPasswordServiceModel);

        /// <summary>
        /// Resets the password for a given user
        /// </summary>
        /// <param name="resetPasswordTokenServiceModel">Model carying the email of the user that needs his password reset and the password token</param>
        Task ResetPasswordAsync(ResetPasswordTokenServiceModel resetPasswordTokenServiceModel);
    }
}
