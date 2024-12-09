using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Services.Models.Event
{
    public class RateEventServiceModel
    {
        public Guid UserId { get; set; }
        public float RatingValue { get; set; }
        public Guid EventId { get; set; }

    }
}
