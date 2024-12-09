using AutoMapper;
using EventManager.Data.Models.Picture;
using EventManager.Services.Models.Picture;
using EventManager.Web.Models.Picture;

namespace EventManager.Web.Setup.Mappings
{
    public class PictureMappings : Profile
    {
        public PictureMappings()
        {
            CreateMap<ProfilePicture, PictureServiceModel>();
            CreateMap<UploadPictureWebModel, UploadPictureServiceModel>();
        }
    }
}
