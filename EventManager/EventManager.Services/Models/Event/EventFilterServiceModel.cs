using EventManager.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Models.Event
{
    public class EventFilterServiceModel
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
