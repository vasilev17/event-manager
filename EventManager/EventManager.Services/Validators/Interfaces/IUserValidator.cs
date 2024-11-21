using EventManager.Services.Models.User;

namespace EventManager.Services.Validators.Interfaces
{
    public interface IUserValidator
    {
        /// <summary>
        /// Validates if the given user model follows the requierments.
        /// </summary>
        /// <param name="model">The user model</param>
        void ValidateRegisterUserModel(RegisterUserServiceModel model);
    }
}
