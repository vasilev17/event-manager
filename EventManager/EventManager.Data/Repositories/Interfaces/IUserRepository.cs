using EventManager.Data.Models;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> AddAsync(User entity);

        Task<User> GetByNameAsync(string name);
    }
}