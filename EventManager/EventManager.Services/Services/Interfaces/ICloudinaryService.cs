using CloudinaryDotNet.Actions;
using EventManager.Services.Models.Picture;

namespace EventManager.Services.Services.Interfaces
{
    public interface ICloudinaryService
    {
        /// <summary>
        /// Uploads picture to cloudinary
        /// </summary>
        /// <param name="model">Contains the data for the picture</param>
        /// <returns>The upload result</returns>
        Task<RawUploadResult> UploadPictureAsync(UploadPictureServiceModel model);

        /// <summary>
        /// Delets a picture
        /// </summary>
        /// <param name="model">The model with the data about the picture that needs to be deleted</param>
        Task DeletePictureAsync(PictureServiceModel model);
    }
}
