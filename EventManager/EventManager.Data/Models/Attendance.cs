using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("Attendances")]
    public class Attendance
    {
        public Guid Id { get; set; }

        public Guid EventId { get; set; }
        public Guid UserId { get; set; }

        public Event Event { get; set; }
        public User User { get; set; }

    }
}
