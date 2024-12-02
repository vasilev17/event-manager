using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Common.Models;
using EventManager.Data.Exceptions;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Exceptions;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly string _localTokenLocation;

        public UserService(IUserRepository userRepository,
            IEmailService emailService,
            IJwtService jwtService,
            IMapper mapper,
            string localTokenLocation)
        {
            _emailService = emailService;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
            _localTokenLocation = localTokenLocation;
        }

        public async Task<TokenModel> LoginAsync(LoginServiceModel loginServiceModel)
        {
            var loggedUser = await _userRepository.LoginAsync(loginServiceModel.UserName, loginServiceModel.Password);
            var roleNames = await _userRepository.GerUserRoleAsync(loggedUser);

            return new TokenModel(_jwtService.GenerateJwtToken(loggedUser.Id, loggedUser.UserName!, roleNames!));
        }

        public async Task<TokenModel> RegisterAsync(RegisterServiceModel userServiceModel)
        {
            var user = _mapper.Map<User>(userServiceModel);

            await _userRepository.AddAsync(user, userServiceModel.Role);

            var createdUser = await _userRepository.GetByUserNameAsync(userServiceModel.UserName);
            var roleNames = await _userRepository.GerUserRoleAsync(createdUser);

            return new TokenModel(_jwtService.GenerateJwtToken(createdUser.Id, createdUser.UserName!, roleNames!));
        }

        public async Task SendResendPasswordAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            var passwordModel = await _userRepository.GeneratePasswordTokenModelAsync(resetPasswordServiceModel.Email);

            var serviceModel = _mapper.Map<UserPasswordResetServiceModel>(passwordModel);

            var result = await _emailService.SendResetPasswordMailAsync(serviceModel);

            if (!result)
                throw new EmailSenderException(ExceptionConstants.CantSendEmail);
        }

        public async Task ResetPasswordAsync(ResetPasswordTokenServiceModel resetPasswordTokenServiceModel)
        {
            var succeeded = await _userRepository.ResetPassword(
                resetPasswordTokenServiceModel.Email,
                resetPasswordTokenServiceModel.Token,
                resetPasswordTokenServiceModel.Password);

            if (!succeeded)
                throw new ResetPasswordException(ExceptionConstants.FailedToRestorePassword);

        }

        public async Task ResendPasswordLocalAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            var token = await _userRepository.GeneratePasswordTokenAsync(resetPasswordServiceModel.Email);

            using (StreamWriter writer = new StreamWriter(_localTokenLocation, append: false))
                writer.WriteLine(token);
        }

        public async Task UpdateUserAsync(Guid id, UpdateUserServiceModel updateUserServiceModel)
        {
            var user = await _userRepository.GetByIdAsync(id);

            PopulateUser(user, updateUserServiceModel);

            var result = await _userRepository.EditAsync(id, user);

            if (!result)
                throw new DatabaseException(ExceptionConstants.FailedToDeleteUser);
        }

        private void PopulateUser(User user, UpdateUserServiceModel updateUserServiceModel)
        {
            if (!string.IsNullOrEmpty(updateUserServiceModel.UserName))
                user.UserName = updateUserServiceModel.UserName;

            if (!string.IsNullOrEmpty(updateUserServiceModel.FirstName))
                user.FirstName = updateUserServiceModel.FirstName;

            if (!string.IsNullOrEmpty(updateUserServiceModel.LastName))
                user.LastName = updateUserServiceModel.LastName;

            if (!string.IsNullOrEmpty(updateUserServiceModel.Email))
                user.Email = updateUserServiceModel.Email;
        }

        public async Task DeleteUserAsync(Guid id)
        {
            var result = await _userRepository.DeleteAsync(id);

            if (!result)
                throw new DatabaseException(ExceptionConstants.FailedToUpdateUser);
        }
    }
}
