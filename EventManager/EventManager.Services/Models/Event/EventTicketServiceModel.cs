namespace EventManager.Services.Models.Event
{
    public class EventTicketServiceModel
    {
        public required Guid EventId { get; set; }
        public string? Type { get; set; }
        public required decimal Price { get; set; }
        public string? Description { get; set; }
    }
}
