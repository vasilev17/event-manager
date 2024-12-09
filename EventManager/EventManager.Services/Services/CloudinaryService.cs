using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using EventManager.Common.Constants;
using EventManager.Services.Exceptions;
using EventManager.Services.Models.Picture;
using EventManager.Services.Services.Interfaces;

namespace EventManager.Services.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryService(string cloudName, string apiKey, string apiSecret)
        {
            _cloudinary = new Cloudinary(new Account(cloudName, apiKey, apiSecret));
        }

        public async Task<RawUploadResult> UploadPictureAsync(UploadPictureServiceModel model)
        {
            RawUploadParams rawUploadParams = new()
            {
                File = new FileDescription(model.FileName, model.Stream),
                PublicId = model.FileName,
                UseFilename = true
            };

            var result = await _cloudinary.UploadAsync(rawUploadParams);

            if (result.Error != null)
                throw new CloudinaryException(string.Format(ExceptionConstants.CloudinaryError, result.Error.Message));

            return result;
        }

        public async Task DeletePictureAsync(PictureServiceModel model)
        {
            var result = await _cloudinary.DeleteResourcesAsync(model.ResourceType, model.PublicId);
            
            if(result.Error != null)
                throw new CloudinaryException(string.Format(ExceptionConstants.CloudinaryError, result.Error.Message));
        }
    }
}
