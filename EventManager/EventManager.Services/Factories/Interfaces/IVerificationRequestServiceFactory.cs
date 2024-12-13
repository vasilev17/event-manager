using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Factories.Interfaces
{
    public interface IVerificationRequestServiceFactory
    {
        IVerificationRequestService Create();
    }
}
