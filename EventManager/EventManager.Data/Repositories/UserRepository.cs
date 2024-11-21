using EventManager.Common.Constants;
using EventManager.Data.Models;
using EventManager.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EventManager.Data.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager) : base(context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public override Task<bool> AddAsync(User entity)
        {
            return AddAsync(entity, RoleConstants.DefaultRole.ToString());
        }

        public async Task<bool> AddAsync(User entity, string roleName)
        {
            entity.PasswordHash = this._userManager.PasswordHasher.HashPassword(entity, entity.PasswordHash!).ToString();
            using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                // Add the user to the database without committing
                var result = await _userManager.CreateAsync(entity);
                if (!result.Succeeded)
                    return false; // Handle error as needed

                // Add the user to a role
                var roleExists = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    // Optionally, create the role if it doesn't exist
                    var roleResult = await _roleManager.CreateAsync(new Role(roleName));
                    if (!roleResult.Succeeded)
                        return false; // Handle error as needed
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(entity, roleName);
                if (!addToRoleResult.Succeeded)
                    return false; // Handle error as needed

                // Commit the transaction
                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                // Rollback the transaction in case of an error
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<User> GetByNameAsync(string username)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
