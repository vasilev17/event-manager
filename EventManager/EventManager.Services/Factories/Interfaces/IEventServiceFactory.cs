using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Factories.Interfaces
{
    public interface IEventServiceFactory
    {
        IEventService Create();
    }
}
