namespace EventManager.Services.Models.User
{
    public class VerificationRequestServiceModel
    {
        public Guid OrganizerId { get; set; }

        public string? Description { get; set; }     
    }
}
