using AutoMapper;
using EventManager.Data.Models;
using EventManager.Services.Models.User;
using EventManager.Web.Models;

namespace EventManager.Web.Setup.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterWebModel, RegisterUserServiceModel>();
            CreateMap<RegisterUserServiceModel, User>();
        }
    }
}
