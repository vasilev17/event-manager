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
        /// Gets the id out of a token
        /// </summary>
        /// <param name="token">The token</param>
        /// <returns>The id</returns>
        Guid GetId(string token);
    }
}
