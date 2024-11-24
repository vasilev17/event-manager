namespace EventManager.Data.Models
{
    public class UserPasswordResetModel
    {
        public required string Token { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required string Email { get; set; }
    }
}
