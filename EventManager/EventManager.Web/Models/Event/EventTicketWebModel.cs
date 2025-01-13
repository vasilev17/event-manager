namespace EventManager.Services.Models.Event
{
    public class EventTicketWebModel
    {
        public required Guid EventId { get; set; }
        public string? Type { get; set; } = "General Admission Ticket (GA)";
        public required decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
