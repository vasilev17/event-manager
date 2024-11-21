using EventManager.Common.Constants;
using EventManager.Data.Models;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User entity);

        Task<bool> AddAsync(User entity, Roles role);

        Task<User> GetByNameAsync(string name);
    }
}