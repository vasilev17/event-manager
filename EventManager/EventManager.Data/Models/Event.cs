using EventManager.Common.Constants;
using EventManager.Data.Models.Picture;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("Events")]
    public class Event
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public EventPicture EventPicture { get; set; } = new() { Url = PictureConstants.DefaultEventPicture };
        public HashSet<EventType> Types { get; set; } = new();
        public string? Webpage { get; set; }
        public required string Address { get; set; }
        public required bool IsActivity { get; set; } = false;
        public required bool IsThirdParty { get; set; } = false;
        public required short Rating { get; set; }

        //TODO Tickets
    }
}
