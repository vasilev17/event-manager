using EventManager.Common.Constants;
using EventManager.Data.Models;
using EventManager.Data.Models.Picture;

namespace EventManager.Services.Models.Event
{
    public class CreateEventServiceModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required DateTime StartDateTime { get; set; }
        public required DateTime EndDateTime { get; set; }
        public required HashSet<EventTypes> Types { get; set; }
        public string? Webpage { get; set; }
        public required string Address { get; set; }
        public bool IsActivity { get; set; }
        public bool IsThirdParty { get; set; }
        public short Rating { get; set; }

        //TODO Tickets

    }
}
