using EventManager.Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace EventManager.Web.Models
{
    public record RegisterWebModel
    {
        public required string Password { get; set; }

        public required string UserName { get; set; }

        public required string Email { get; set; }
        
        public required string FirstName { get; set; }
        
        public required string LastName { get; set; }

        [EnumDataType(typeof(Roles))]
        public required Roles Role { get; set; }
    }
}
