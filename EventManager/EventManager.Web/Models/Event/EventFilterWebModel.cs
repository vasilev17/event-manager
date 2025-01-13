using EventManager.Common.Constants;

namespace EventManager.Web.Models.Event
{
    public class EventFilterWebModel
    {
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public HashSet<EventTypes>? EventTypes { get; set; }
    }
}
