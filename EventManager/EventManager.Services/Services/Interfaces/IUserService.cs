using EventManager.Common.Models;
using EventManager.Services.Models.Picture;
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
        /// Gets an organizator by name
        /// </summary>
        /// <param name="organizatorName">The name of the orgnizator</param>
        /// <returns>The organizator</returns>
        Task<GetUserServiceModel> GetOrganizerAsync(string oganizerName);

        /// <summary>
        /// Generates password token for a user and sends him an email with it
        /// </summary>
        /// <param name="resetPasswordServiceModel">The model carying the data</param>
        Task SendResendPasswordAsync(ResetPasswordServiceModel resetPasswordServiceModel);

        /// <summary>
        /// Generates password token for a user and stores it in a local file
        /// </summary>
        /// <param name="resetPasswordServiceModel">The model carying the data</param>
        Task ResendPasswordLocalAsync(ResetPasswordServiceModel resetPasswordServiceModel);

        /// <summary>
        /// Resets the password for a given user
        /// </summary>
        /// <param name="resetPasswordTokenServiceModel">Model carying the email of the user that needs his password reset and the password token</param>
        Task ResetPasswordAsync(ResetPasswordTokenServiceModel resetPasswordTokenServiceModel);

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="id">Id of the user</param>
        /// <param name="updateUserServiceModel">Model carying the new user data</param>
        Task UpdateUserAsync(Guid id, UpdateUserServiceModel updateUserServiceModel);

        /// <summary>
        /// Uploading profile picture to cloudinary and safing it to the db
        /// </summary>
        /// <param name="profilePictureServiceModel"></param>
        /// <returns></returns>
        Task UploadProfilePictureAsync(ProfilePictureServiceModel profilePictureServiceModel);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">The id of the user to be deleted</param>
        Task DeleteUserAsync(Guid id);

        /// <summary>
        /// Deletes the profile picture of the user
        /// </summary>
        /// <param name="id">Id of the user</param>
        Task DeleteProfilePictureAsync(Guid id);
    }
}
