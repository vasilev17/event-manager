namespace EventManager.Common.Models
{
    public class EventGridViewDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime? StartDateTime { get; set; }
        public string? EventPictureUrl { get; set; }
        public string Address { get; set; }

    }
}
