using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EventManager.Web.Models
{
    public class RegisterWebModel
    {
        [NotNull]
        public required string Password { get; set; }

        [NotNull]
        public required string UserName { get; set; }

        [NotNull]
        public required string Email { get; set; }

        [NotNull]
        public required string FirstName { get; set; }

        [NotNull]
        public required string LastName { get; set; }
    }
}
