namespace EventManager.Services.Services.Interfaces
{
    public interface IJwtService
    {
        string GenerateJwtToken(Guid userId, string username, List<string> roleNames);
    }
}
