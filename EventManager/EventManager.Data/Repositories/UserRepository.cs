using EventManager.Common.Constants;
using EventManager.Data.Exceptions;
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
            return AddAsync(entity, RoleConstants.DefaultRole);
        }

        public async Task<bool> AddAsync(User entity, Roles role)
        {
            entity.PasswordHash = this._userManager.PasswordHasher.HashPassword(entity, entity.PasswordHash!).ToString();
            using var transaction = await DbContext.Database.BeginTransactionAsync();
            try
            {
                // Add the user to the database without committing
                var result = await _userManager.CreateAsync(entity);
                if (!result.Succeeded)
                    throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "user"));

                // Add the user to a role
                var roleExists = await _roleManager.RoleExistsAsync(role.ToString());
                if (!roleExists)
                {
                    // Optionally, create the role if it doesn't exist
                    var roleResult = await _roleManager.CreateAsync(new Role(role.ToString()));
                    if (!roleResult.Succeeded)
                        throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "role"));
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(entity, role.ToString());
                if (!addToRoleResult.Succeeded)
                    throw new DatabaseException(ExceptionConstants.CantAddToRole);

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

        public async Task<User> LoginAsync(string userName, string password)
        {
            var user = await GetByUserNameAsync(userName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                throw new InvalidDataException(ExceptionConstants.InvalidCredentials);

            return user;
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public async Task<UserPasswordResetModel> GeneratePasswordToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return null;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new UserPasswordResetModel
            {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }
    }
}
