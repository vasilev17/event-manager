using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("EventTypes")]
    public class EventType
    {
        public required Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }

        public HashSet<Event> Events { get; set; } = new();

        public EventType(string name) { this.Name = name; }
    }
}
