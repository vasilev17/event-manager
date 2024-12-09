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

        #region Add
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
                    throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "user") + "Inner exception: " + result.Errors.ToString());

                // Add the user to a role
                var roleExists = await _roleManager.RoleExistsAsync(role.ToString());
                if (!roleExists)
                {
                    // Optionally, create the role if it doesn't exist
                    var roleResult = await _roleManager.CreateAsync(new Role(role.ToString()));
                    if (!roleResult.Succeeded)
                        throw new CreationDatabaseException(string.Format(ExceptionConstants.CanNotCreate, "role") + "Inner exception: " + roleResult.Errors.ToString());
                }

                var addToRoleResult = await _userManager.AddToRoleAsync(entity, role.ToString());
                if (!addToRoleResult.Succeeded)
                    throw new DatabaseException(ExceptionConstants.CantAddToRole + "Inner exception: " + addToRoleResult.Errors.ToString());

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
        #endregion

        #region Read
        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _userManager.Users
                .Include(u => u.Roles)
                .FirstOrDefaultAsync(x => x.UserName == username);
        }

        public override async Task<User> GetByIdAsync(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user == null)
                throw new ArgumentException(ExceptionConstants.UserNotFound);

            return user;
        }

        public async Task<IList<string>> GerUserRoleAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        #endregion

        #region Authentication
        public async Task<bool> ResetPassword(string email, string token, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                return false;

            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            return result.Succeeded;
        }

        public async Task<User> LoginAsync(string userName, string password)
        {
            var user = await GetByUserNameAsync(userName);

            if (user == null || !await _userManager.CheckPasswordAsync(user, password))
                throw new InvalidDataException(ExceptionConstants.InvalidCredentials);

            return user;
        }

        public async Task<UserPasswordResetModel> GeneratePasswordTokenModelAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new DatabaseException(ExceptionConstants.UserNotFound);

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return new UserPasswordResetModel
            {
                Email = email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = token
            };
        }

        public async Task<string> GeneratePasswordTokenAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
                throw new DatabaseException(ExceptionConstants.UserNotFound);

            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        #endregion

        #region Update
        public override async Task<bool> EditAsync(Guid id, User newEntity)
        {
            newEntity.Id = id;
            IdentityResult result = await _userManager.UpdateAsync(newEntity);

            return result.Succeeded;
        }
        #endregion

        #region Delete
        public async Task DeleteUserAsync(string userName)
        {
            var user = await GetByUserNameAsync(userName);

            var result = await _userManager.DeleteAsync(user);
        }

        public override async Task<bool> DeleteAsync(User user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }
        #endregion
    }
}
