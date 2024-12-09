namespace EventManager.Services.Models.Picture
{
    public class UploadPictureServiceModel
    {
        public required string FileName { get; set; }

        public required Stream Stream { get; set;}
    }
}
