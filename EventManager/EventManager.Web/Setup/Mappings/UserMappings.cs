using AutoMapper;
using EventManager.Data.Models;
using EventManager.Services.Models.User;
using EventManager.Web.Models.User;

namespace EventManager.Web.Setup.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterWebModel, RegisterServiceModel>();
            CreateMap<LoginWebModel, LoginServiceModel>();
            CreateMap<ResetPasswordWebModel, ResetPasswordServiceModel>();

            CreateMap<RegisterServiceModel, User>()
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
        }
    }
}
