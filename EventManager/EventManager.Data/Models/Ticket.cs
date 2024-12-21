using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EventManager.Data.Models
{
    [Table("Tickets")]
    public class Ticket
    {
        public required Guid Id { get; set; }
        public required string Barcode { get; set; } = Guid.NewGuid().ToString();
        public required Guid EventId { get; set; }
        public Guid? UserId { get; set; } = null; // Nullable because AvailableTickets are not booked by a user yet
        public string Type { get; set; } = "General Admission Ticket (GA)";
        public required decimal Price { get; set; } = 0;
        public string? Description { get; set; }
        public required DateTime CreationDate { get; set; }
        public DateTime? BookingDate { get; set; } = null; // Nullable because AvailableTickets are not booked yet
       
        [JsonIgnore]
        public Event Event { get; set; }

        [JsonIgnore]
        public User User { get; set; }
    }
}
