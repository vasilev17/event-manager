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
