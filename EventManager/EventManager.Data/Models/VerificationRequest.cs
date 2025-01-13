namespace EventManager.Data.Models
{
    public class VerificationRequest
    {
        public Guid Id { get; set; }

        public User Organizer { get; set; }

        public bool IsCompleated { get; set; }

        public string Description { get; set; }
    }
}
