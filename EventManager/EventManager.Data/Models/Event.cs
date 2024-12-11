using EventManager.Common.Constants;
using EventManager.Data.Models.Picture;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("Events")]
    public class Event
    {
        public required Guid Id { get; set; }
        public required User User { get; set; }
        public Guid UserId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public EventPicture EventPicture { get; set; } = new() { Url = PictureConstants.DefaultEventPicture };
        public HashSet<EventType> Types { get; set; } = new();
        public string? Webpage { get; set; }
        public required string Address { get; set; }
        public bool IsActivity { get; set; } = false;
        public bool IsThirdParty { get; set; } = false;
        public List<Rating> Ratings { get; set; } = new();
        public float AverageRating { get; set; } = 0f;
        public decimal? MinPrice { get; set; } = null;
        public decimal MaxPrice { get; set; } = 0;
        public HashSet<User> Attendees { get; set; } = new();
        public List<Ticket> AvailableTickets { get; set; } = new();

    }
}
