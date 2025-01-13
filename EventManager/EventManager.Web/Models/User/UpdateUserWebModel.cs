using System.ComponentModel;

namespace EventManager.Web.Models.User
{
    public class UpdateUserWebModel
    {
        public required string? UserName { get; set; }

        public required string? Email { get; set; }

        public required string? FirstName { get; set; }

        public required string? LastName { get; set; }
    }
}
