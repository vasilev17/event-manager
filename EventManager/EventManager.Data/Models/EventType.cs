using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventManager.Data.Models
{
    [Table("EventTypes")]
    public class EventType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public EventType() { }

        public EventType(string name) { this.Name = name; }

        public HashSet<Event> Events { get; set; } = new();


    }
}
