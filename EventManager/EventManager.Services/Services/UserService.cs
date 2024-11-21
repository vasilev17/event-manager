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
            
            await _userRepository.AddAsync(user, userServiceModel.Role);

            var createdUser = await _userRepository.GetByNameAsync(userServiceModel.UserName);
            List<string?> roleNames = createdUser.Roles.Select(x => x.Name).ToList();

            return new TokenModel(_jwtService.GenerateJwtToken(createdUser.Id, createdUser.UserName!, roleNames!));
        }
    }
}
