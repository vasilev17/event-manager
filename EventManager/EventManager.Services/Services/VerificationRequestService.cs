using AutoMapper;
using EventManager.Common.Constants;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;
using System.Security;

namespace EventManager.Services.Services
{
    public class VerificationRequestService : IVerificationRequestService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IVerificationRequestsRepository _verificationRequestsRepository;

        public VerificationRequestService(IUserRepository userRepository, 
            IEventRepository eventRepository,
            IVerificationRequestsRepository verificationRequestsRepository,
            IMapper mapper) 
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _verificationRequestsRepository = verificationRequestsRepository;
            _mapper = mapper;
        }

        public async Task RequestVerification(VerificationRequestServiceModel model)
        {
            var organizer = await _userRepository.GetByIdAsync(model.OrganizerId);

            if (await _userRepository.IsInRoleAsync(organizer, Roles.Verified.ToString()))
                throw new VerificationException(ExceptionConstants.OrganizerIsVerified);

            var entity = _mapper.Map<VerificationRequest>(model);
            entity.IsCompleated = false;

            entity.Organizer = organizer;

            await _verificationRequestsRepository.AddAsync(entity);
        }

        public async Task<IEnumerable<VerificationRequestInfo>> GetAllActiveRequestsAsync()
        {
            var requests = _verificationRequestsRepository.GetAllActiveRequests();
            var infos = new List<VerificationRequestInfo>(requests.Count());

            foreach (var request in requests)
            {
                var info = _mapper.Map<VerificationRequestInfo>(request);
                var orgEvents  = await _eventRepository.GetAllEventsByOrganizerAsync(request.Organizer.Id);

                info.EventsCount = orgEvents.Count();
                infos.Add(info);
            }

            return infos;
        }

        public async Task CompleateRequestAsync(Guid id)
        {
            var request = await _verificationRequestsRepository.GetByIdAsync(id);

            await _userRepository.AddToRoleAsync(request.Organizer, Roles.Verified);

            request.IsCompleated = true;
        }

        public async Task DeleteRequestAsync(Guid id)
        {
            var entity = await _verificationRequestsRepository.GetByIdAsync(id);
            await _verificationRequestsRepository.DeleteAsync(entity);
        }
    }
}
