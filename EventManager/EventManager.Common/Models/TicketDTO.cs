using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Common.Models
{
    public class TicketDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
