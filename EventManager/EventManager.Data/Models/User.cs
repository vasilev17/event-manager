using EventManager.Common.Constants;
using EventManager.Data.Models.Picture;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventManager.Data.Models
{
    [Table("Users")]
    public class User : IdentityUser<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ProfilePicture ProfilePicture { get; set; } = new() { Url = PictureConstants.DefaultUserPicture };

        public HashSet<Role> Roles { get; set; } = new();

        [JsonIgnore]
        public HashSet<Event> Events { get; set; } = new();
    }
}
