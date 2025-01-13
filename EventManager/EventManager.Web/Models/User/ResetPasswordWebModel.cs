using System.ComponentModel.DataAnnotations;

namespace EventManager.Web.Models.User
{
    public class ResetPasswordWebModel
    {
        public required string Email { get; set; }
    }
}
