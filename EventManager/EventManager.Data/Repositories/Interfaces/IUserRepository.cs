using EventManager.Common.Constants;
using EventManager.Data.Models;

namespace EventManager.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        /// <summary>
        /// Adds a user to the database with the default role.
        /// </summary>
        /// <param name="entity">The entity with the user data</param>
        /// <returns>True if the user is added correctly</returns>
        Task<bool> AddAsync(User entity);

        /// <summary>
        /// Adds a user to the database with the default role. If one operation fails (adding of the user, role assignment/creation) all fail
        /// </summary>
        /// <param name="entity">The entity with the user data</param>
        /// <returns>True if the user is added correctly</returns>
        Task<bool> AddAsync(User entity, Roles role);

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
    }
}