using EventManager.Common.Models;
using EventManager.Services.Models.User;

namespace EventManager.Services.Services.Interfaces
{
    public interface IUserService
    {
        Task<TokenModel> RegisterUser(UserServiceModel user);
    }
}
