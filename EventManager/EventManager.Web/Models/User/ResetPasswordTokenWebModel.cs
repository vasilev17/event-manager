namespace EventManager.Web.Models.User
{
    public class ResetPasswordTokenWebModel : ResetPasswordWebModel
    {
        public required string Token { get; set; }

        public required string Password { get; set; }

        public required string PasswordConfirm { get; set; }
    }
}
