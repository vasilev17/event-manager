using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PictureURL { get; set; }

        public HashSet<Role> Roles { get; set; } = new();
    }
}
