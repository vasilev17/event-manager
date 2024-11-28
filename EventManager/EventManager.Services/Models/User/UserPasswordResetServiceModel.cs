namespace EventManager.Services.Models.User
{
    public class UserPasswordResetServiceModel
    {
        public string? Email { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Token { get; set; }
    }
}
