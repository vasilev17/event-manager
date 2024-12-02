namespace EventManager.Services.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(Guid userId, string username, IList<string> roleNames);

        bool ValidateJwtToken(Guid userId, string jwtToken);
    }
}
