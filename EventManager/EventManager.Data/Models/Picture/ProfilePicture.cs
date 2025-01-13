namespace EventManager.Data.Models.Picture
{
    public class ProfilePicture : Picture
    {
        public User User { get; set; }

        public Guid UserId { get; set; }
    }
}
