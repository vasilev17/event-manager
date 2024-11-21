using EventManager.Common.Constants;

namespace EventManager.Services.Models.User
{
    public record RegisterUserServiceModel
    {
        public required string Password { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }

        public required string FirstName { get; set; }

        public required string LastName { get; set; }

        public required Roles Role { get; set;}
    }
}
