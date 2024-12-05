using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Data.Models.Picture
{
    public class EventPicture : Picture
    {
        public Event Event { get; set; }

        public Guid EventId { get; set; }
    }
}
