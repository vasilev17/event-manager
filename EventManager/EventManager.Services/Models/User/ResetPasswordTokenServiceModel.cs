namespace EventManager.Services.Models.User
{
    public class ResetPasswordTokenServiceModel : ResetPasswordServiceModel
    {
        public required string Token { get; set; }

        public required string Password { get; set; }

        public required string PasswordConfirm { get; set; }
    }
}
