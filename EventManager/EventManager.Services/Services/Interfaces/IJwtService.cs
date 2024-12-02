namespace EventManager.Services.Services.Interfaces
{
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT for the user and adds it's claims
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="username">The username of the user</param>
        /// <param name="roleNames">The roles of the user</param>
        /// <returns>The token</returns>
        string GenerateJwtToken(Guid userId, string username, IList<string> roleNames);

        /// <summary>
        /// Validates that the token belongs to the user 
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <param name="jwtToken">The token</param>
        /// <returns>Bool showing if the token belongs to the user</returns>
        bool ValidateJwtToken(Guid userId, string jwtToken);
    }
}
