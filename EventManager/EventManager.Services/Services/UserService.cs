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
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IJwtService jwtService, IMapper mapper)
        {
            _jwtService = jwtService;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<TokenModel> RegisterUser(RegisterUserServiceModel userServiceModel)
        {
            User user = _mapper.Map<User>(userServiceModel);
            var userResult = await _userRepository.AddAsync(user);

            if (!userResult)
                throw new ArgumentException();

            var craetedUser = await _userRepository.GetByNameAsync(userServiceModel.UserName);

            return new TokenModel(_jwtService.GenerateJwtToken(craetedUser.Id, craetedUser.UserName!, new List<string>()));
        }
    }
}
