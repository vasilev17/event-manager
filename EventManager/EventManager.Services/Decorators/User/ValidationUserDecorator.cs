using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Services.Models.Picture;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using System.Text.RegularExpressions;

namespace EventManager.Services.Decorators.User
{
    public class ValidationUserDecorator : IUserService
    {
        private readonly IUserService _parent;
        private const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private const string SpecialCharecters = "!@#$%^&*()_+<>,:;'\"{}[]";

        public ValidationUserDecorator(IUserService parent)
        {
            _parent = parent;
        }

        public Task<TokenModel> LoginAsync(LoginServiceModel loginServiceModel)
        {
            ValidateLogin(loginServiceModel);

            return _parent.LoginAsync(loginServiceModel);
        }

        private void ValidateLogin(LoginServiceModel model)
        {
            if(PropertiesAreNull(model))
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);
        }

        public Task<TokenModel> RegisterAsync(RegisterServiceModel user)
        {
            ValidateRegisterModel(user);

            return _parent.RegisterAsync(user);
        }

        private void ValidateRegisterModel(RegisterServiceModel user)
        {
            ValidatePassword(user);

            if (user.Role != Roles.User && user.Role != Roles.Organizer && user.Role != Roles.Admin)
                throw new ArgumentException(ExceptionConstants.InvalidRole);

            if (PropertiesAreNull(user))
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);
        }

        public Task SendResendPasswordAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            ValidateSendResentPassword(resetPasswordServiceModel);

            return _parent.SendResendPasswordAsync(resetPasswordServiceModel);
        }

        public Task ResendPasswordLocalAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            ValidateSendResentPassword(resetPasswordServiceModel);

            return _parent.ResendPasswordLocalAsync(resetPasswordServiceModel);
        }

        private void ValidateSendResentPassword(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            if (PropertiesAreNull(resetPasswordServiceModel))
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);

            ValidateEmail(resetPasswordServiceModel.Email);
        }

        public Task ResetPasswordAsync(ResetPasswordTokenServiceModel resetPasswordTokenServiceModel)
        {
            ValidateResetPassword(resetPasswordTokenServiceModel);

            return _parent.ResetPasswordAsync(resetPasswordTokenServiceModel);
        }

        private void ValidateResetPassword(ResetPasswordTokenServiceModel resetPasswordTokenServiceModel)
        {
            if (PropertiesAreNull(resetPasswordTokenServiceModel))
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);

            if (resetPasswordTokenServiceModel.Password != resetPasswordTokenServiceModel.PasswordConfirm)
                throw new ArgumentException(ExceptionConstants.PasswordMissMatch);

            ValidateEmail(resetPasswordTokenServiceModel.Email);
        }

        public Task UpdateUserAsync(Guid id, UpdateUserServiceModel updateUserServiceModel)
        {
            if (!string.IsNullOrEmpty(updateUserServiceModel.Email))
                ValidateEmail(updateUserServiceModel.Email);

            return _parent.UpdateUserAsync(id, updateUserServiceModel);
        }

        public Task DeleteUserAsync(Guid id)
        {
            return _parent.DeleteUserAsync(id);
        }

        private void ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, EmailRegex))
                throw new ArgumentException(string.Format(ExceptionConstants.InvalidEmailFormat, email));
        }

        private void ValidatePassword(RegisterServiceModel user)
        {
            if (user.Password.Length < 8)
                throw new ArgumentException(ExceptionConstants.InvalidPasswordSize);

            var passwordRequiermetns = new bool[4];
            passwordRequiermetns[0] = user.Password.Any(char.IsUpper);
            passwordRequiermetns[1] = user.Password.Any(char.IsLower);
            passwordRequiermetns[2] = user.Password.Any(char.IsNumber);
            passwordRequiermetns[3] = user.Password.Any(SpecialCharecters.Contains);

            if (passwordRequiermetns.Count(b => b) < 3)
                throw new ArgumentException(ExceptionConstants.PasswordSpecialCharecters);
        }

        private bool PropertiesAreNull(object obj)
        {
            return !obj.GetType()
                  .GetProperties()
                  .All(prop => prop.GetValue(obj) != null);
        }

        public Task UploadProfilePictureAsync(ProfilePictureServiceModel profilePictureServiceModel)
        {
            return _parent.UploadProfilePictureAsync(profilePictureServiceModel);
        }

        public Task DeleteProfilePictureAsync(Guid id)
        {
            return _parent.DeleteProfilePictureAsync(id);
        }

        public Task<GetUserServiceModel> GetOrganizerAsync(string oganizerName)
        {
            if(oganizerName == null)
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);

            return _parent.GetOrganizerAsync(oganizerName);
        }
    }
}
