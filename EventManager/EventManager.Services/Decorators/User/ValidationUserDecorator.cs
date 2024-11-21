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
        private const string SpecialCharecters = "!@#$%^&*()_+<>,:;'\"{}[]";

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
            ValidatePassword(user);

            if (!Regex.IsMatch(user.Email, EmailRegex))
                throw new ArgumentException(string.Format(ExceptionConstants.InvalidEmailFormat, user.Email));

            if (PropertiesAreNull(user))
                throw new ArgumentException(ExceptionConstants.AllPropertiesRequiered);
        }

        private void ValidatePassword(RegisterUserServiceModel user)
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
    }
}
