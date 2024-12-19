using AutoMapper;
using EventManager.Common.Models;
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
            CreateMap<RateEventWebModel, RateEventServiceModel>();
            CreateMap<EventTicketWebModel, EventTicketServiceModel>();

            CreateMap<CreateEventServiceModel, Event>()
                .ForMember(dest => dest.Types, opt => opt.MapFrom(src => src.Types.Select(t => new EventType(t.ToString())).ToHashSet()));
            CreateMap<RateEventServiceModel, Rating>();
            CreateMap<EventTicketServiceModel, Ticket>();

            CreateMap<EventDTO, EventGridViewDTO>();
            CreateMap<Ticket, TicketDTO>();

            CreateMap<Event, EventDTO>()
            .ForMember(dest => dest.EventTypeNames, opt => opt.MapFrom(src => new HashSet<string>(src.Types.Select(t => t.Name))))
            .ForMember(dest => dest.CreatorUsername, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.CreatorProfilePictureURL, opt => opt.MapFrom(src => src.User.ProfilePicture.Url))
            .ForMember(dest => dest.AvailableTickets, opt => opt.MapFrom((src, dest, _, context) =>
            {
                return src.AvailableTickets
                    .Where(at => at.UserId == null)
                    .Select(at => context.Mapper.Map<TicketDTO>(at)) // Map each Ticket to TicketDTO
                    .ToList();
            }));




        }

    }
}
