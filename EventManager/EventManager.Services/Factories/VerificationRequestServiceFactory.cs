using AutoMapper;
using EventManager.Data.Repositories;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Factories.Interfaces;
using EventManager.Services.Services;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Factories
{
    public class VerificationRequestServiceFactory : IVerificationRequestServiceFactory
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVerificationRequestsRepository _verificationRequestsRepository;

        public VerificationRequestServiceFactory(IUserRepository userRepository,
            IEventRepository eventRepository,
            IVerificationRequestsRepository verificationRequestsRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _verificationRequestsRepository = verificationRequestsRepository;
            _mapper = mapper;
        }

        public IVerificationRequestService Create()
        {
            return new VerificationRequestService(_userRepository, _eventRepository, _verificationRequestsRepository, _mapper);
        }
    }
}
