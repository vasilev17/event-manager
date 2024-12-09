using EventManager.Common.Constants;
using EventManager.Data.Models;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// Adds a user to the database with the default role. If one operation fails (adding of the user, role assignment/creation) all fail
        /// </summary>
        /// <param name="entity">The entity with the user data</param>
        /// <returns>True if the user is added correctly</returns>
        Task<bool> AddAsync(User entity, Roles role);

        /// <summary>
        /// Generates a password token model for a user
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>The token password</returns>
        Task<UserPasswordResetModel> GeneratePasswordTokenModelAsync(string email);

        /// <summary>
        /// Generates a password token for a user
        /// </summary>
        /// <param name="email">The email of the user</param>
        /// <returns>The token password</returns>
        Task<string> GeneratePasswordTokenAsync(string email);

        /// <summary>
        /// Resets the password of a user
        /// </summary>
        /// <param name="email">t</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ResetPassword(string email, string token, string newPassword);

        /// <summary>
        /// Gets a user from the database using his user name
        /// </summary>
        /// <param name="userName">The user name</param>
        /// <returns>The user</returns>
        Task<User> GetByUserNameAsync(string userName);

        /// <summary>
        /// Logs user in. Checks the password and throws exception if it is not valid.
        /// </summary>
        /// <param name="userName">The user name of the user, trying to log</param>
        /// <param name="password">The password of the user, trying to log</param>
        /// <returns>The logged in user</returns>
        /// <throws>InvalidDataException if the password does not match</throws>
        Task<User> LoginAsync(string userName, string password);

        /// <summary>
        /// Gets the role of a user
        /// </summary>
        /// <param name="user">The user</param>
        /// <returns>List of the user's roles</returns>
        Task<IList<string>> GerUserRoleAsync(User user);

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id">user id</param>
        Task<bool> DeleteAsync(Guid id);

        /// <summary>
        /// Checks if the user is in the given role
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleName"></param>
        /// <returns></returns>
        Task<bool> IsInRoleAsync(User user, string roleName);
    }
}