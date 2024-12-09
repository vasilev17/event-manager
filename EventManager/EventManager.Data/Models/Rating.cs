using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("Ratings")]
    public class Rating
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required User User { get; set; }
        public required float RatingValue { get; set; }
        public Guid EventId { get; set; }
        public required Event Event { get; set; }

    }
}
