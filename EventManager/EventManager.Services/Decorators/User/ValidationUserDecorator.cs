using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using System.Text.RegularExpressions;

namespace EventManager.Services.Decorators.User
{
    public class ValidationUserDecorator : IUserService
    {
        private readonly IUserService _parent;
        private const string EmailRegex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        private const string PasswordPattern = @"
        ^                 # Start of string
        (?=.*[A-Z])       # At least one uppercase letter
        (?=.*[a-z])       # At least one lowercase letter
        (?=.*\d)          # At least one digit
        (?=.*[!@#$%^&*(),.?""{}|<>~`_+=\-]) # At least one special character
        .{8,}             # At least 8 characters long
        ";

        public ValidationUserDecorator(IUserService parent)
        {
            _parent = parent;
        }

        public Task<TokenModel> RegisterUser(RegisterUserServiceModel user)
        {
            ValidateRegisterModel(user);

            return _parent.RegisterUser(user);
        }

        private void ValidateRegisterModel(RegisterUserServiceModel user)
        {
            if (!Regex.IsMatch(user.Email, EmailRegex))
                throw new ArgumentException(string.Format(ExceptionConstants.InvalidEmailFormat, user.Email));

            if (!Regex.IsMatch(user.Password, PasswordPattern))
                throw new ArgumentException(ExceptionConstants.InvalidPasswordFormat);

            if (PropertiesAreNull(user))
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);
        }

        private bool PropertiesAreNull(object obj)
        {
            return obj.GetType()
                  .GetProperties()
                  .All(prop => prop.GetValue(obj) != null);
        }
    }
}
