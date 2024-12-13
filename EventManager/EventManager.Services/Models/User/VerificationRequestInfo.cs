namespace EventManager.Services.Models.User
{
    public class VerificationRequestInfo
    {
        public Guid Id { get; set; }

        public string Description { get; set; }

        public GetUserServiceModel Organizer { get; set; }

        public int EventsCount { get; set; }
    }
}
