using AutoMapper;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Decorators.User;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Factories
{
    public class UserServiceFactory : IUserServiceFactory
    {
        private readonly IUserRepository _repository;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly string _localTokenLocation;

        public UserServiceFactory(IUserRepository userRepository, 
            IEmailService emailService, 
            IJwtService jwtService, 
            IMapper mapper,
            string localTokenlocation)
        {
            _repository = userRepository;
            _jwtService = jwtService;
            _mapper = mapper;
            _emailService = emailService;
            _localTokenLocation = localTokenlocation;
        }

        public IUserService CreateUserService()
        {
            var coreService = new UserService(_repository, _emailService, _jwtService, _mapper, _localTokenLocation);
            return new ValidationUserDecorator(coreService);
        }
    }
}
