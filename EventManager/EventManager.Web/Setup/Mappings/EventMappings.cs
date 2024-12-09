using AutoMapper;
using EventManager.Data.Models;
using EventManager.Data.Models.Picture;
using EventManager.Services.Models.Event;
using EventManager.Services.Models.User;
using EventManager.Services.Services;
using EventManager.Web.Models.Event;

namespace EventManager.Web.Setup.Mappings
{
    public class EventMappings : Profile
    {
        public EventMappings()
        {

            CreateMap<CreateEventWebModel, CreateEventServiceModel>();
            CreateMap<EventFilterWebModel, EventFilterServiceModel>();

            CreateMap<CreateEventServiceModel, Event>()
                .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.Select(t => new EventType(t.ToString())).ToHashSet()));

        }

    }
}
