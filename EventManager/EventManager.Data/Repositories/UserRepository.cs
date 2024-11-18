using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<bool> AddAsync(User entity)
        {
            IdentityResult result = await _userManager.CreateAsync(entity);
            return result.Succeeded;
        }

        public async Task<User> GetByNameAsync(string username)
        {
            return await this._userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
