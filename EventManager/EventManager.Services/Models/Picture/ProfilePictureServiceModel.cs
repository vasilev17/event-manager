namespace EventManager.Services.Models.Picture
{
    public class ProfilePictureServiceModel
    {
        public Guid UserId { get; set; }

        public UploadPictureServiceModel Picture { get; set; }
    }
}
