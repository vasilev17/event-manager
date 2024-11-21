using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("Roles")]
    public class Role : IdentityRole<Guid>
    {
        public Role() { }

        public Role(string name) : base(name) { }

        public HashSet<User> Users { get; set; } = new();
    }
}
