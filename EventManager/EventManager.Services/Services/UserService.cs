using AutoMapper;
using EventManager.Common.Models;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
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

        public UserService(IUserRepository userRepository, IEmailService emailService, IJwtService jwtService, IMapper mapper)
        {
            _emailService = emailService;
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TokenModel> LoginAsync(LoginServiceModel loginServiceModel)
        {
            var loggedUser = await _userRepository.LoginAsync(loginServiceModel.UserName, loginServiceModel.Password);
            var roleNames = loggedUser.Roles.Select(x => x.Name).ToList();

            return new TokenModel(_jwtService.GenerateJwtToken(loggedUser.Id, loggedUser.UserName!, roleNames!));
        }

        public async Task<TokenModel> RegisterAsync(RegisterServiceModel userServiceModel)
        {
            var user = _mapper.Map<User>(userServiceModel);
            
            await _userRepository.AddAsync(user, userServiceModel.Role);

            var createdUser = await _userRepository.GetByUserNameAsync(userServiceModel.UserName);
            var roleNames = createdUser.Roles.Select(x => x.Name).ToList();

            return new TokenModel(_jwtService.GenerateJwtToken(createdUser.Id, createdUser.UserName!, roleNames!));
        }

        public async Task SendResendPasswordAsync(ResetPasswordServiceModel resetPasswordServiceModel)
        {
            var token = await _userRepository.GeneratePasswordToken(resetPasswordServiceModel.Email);

            var result = await _emailService.SendResetPasswordMailAsync(token);
        }
    }
}
