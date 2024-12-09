using AutoMapper;
using EventManager.Data.Repositories;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Decorators.User;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Factories
{
    public class UserServiceFactory : IUserServiceFactory
    {
        private readonly IUserRepository _userRepository;
        private readonly IProfilePictureRepository _profilePictureRepository;
        private readonly IJwtService _jwtService;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly string _localTokenLocation;

        public UserServiceFactory(IUserRepository userRepository, 
            IProfilePictureRepository profilePictureRepository,
            IEmailService emailService, 
            ICloudinaryService cloudinaryService,
            IJwtService jwtService, 
            IMapper mapper,
            string localTokenlocation)
        {
            _userRepository = userRepository;
            _profilePictureRepository = profilePictureRepository;
            _jwtService = jwtService;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
            _emailService = emailService;
            _localTokenLocation = localTokenlocation;
        }

        public IUserService CreateUserService()
        {
            var coreService = new UserService(_userRepository, _profilePictureRepository, _emailService, _jwtService, _cloudinaryService, _mapper, _localTokenLocation);
            return new ValidationUserDecorator(coreService);
        }
    }
}
