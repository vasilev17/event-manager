using SendGrid.Helpers.Mail;
using SendGrid;
using EventManager.Services.Models.User;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _apiKey;
        private readonly string _senderMail;
        private readonly string _senderName;
        private readonly Uri _resetPasswordUri;

        private const string ResetPasswordSubject = "Password reset!";
        private const string ResetPasswordContent = "Open link to reset password: ";

        public EmailService(string apiKey, string senderMail, string senderName, string resetPasswordUri)
        {
            _apiKey = apiKey;
            _senderMail = senderMail;
            _senderName = senderName;
            _resetPasswordUri = new Uri("https://www.google.com/");
        }

        public async Task<bool> SendResetPasswordMailAsync(UserPasswordResetServiceModel emailMode)
        {
            var client = new SendGridClient(_apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(_senderMail, _senderName),
                Subject = ResetPasswordSubject,
                PlainTextContent = ResetPasswordContent + emailMode.Token
            };

            msg.AddTo(new EmailAddress(emailMode.Email, emailMode.FirstName + " " + emailMode.LastName));

            var result = await client.SendEmailAsync(msg);
            return result.IsSuccessStatusCode;
        }
    }
}
