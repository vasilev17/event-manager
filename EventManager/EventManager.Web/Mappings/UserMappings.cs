using AutoMapper;
using EventManager.Data.Models;
using EventManager.Services.Models.User;
using EventManager.Web.Models;

namespace EventManager.Web.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            CreateMap<RegisterWebModel, UserServiceModel>();
            CreateMap<UserServiceModel, User>();
        }
    }
}
