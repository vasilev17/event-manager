using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManager.Common.Models
{
    public class EventDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string? EventPictureUrl { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public string? Webpage { get; set; }
        public string Address { get; set; }
        public bool IsActivity { get; set; }
        public bool IsThirdParty { get; set; }
        public float AverageRating { get; set; }
        public string? CreatorUsername { get; set; }
        public string? CreatorProfilePictureURL { get; set; }
        public HashSet<string> EventTypeNames { get; set; }
        public List<TicketDTO> AvailableTickets { get; set; }


    }
}
