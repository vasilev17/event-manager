using EventManager.Services.Models.User;

namespace EventManager.Services.Services.Interfaces
{
    public interface IEmailService
    {
        Task<bool> SendResetPasswordMailAsync(SendEmailServiceModel emailMode);    
    }
}
