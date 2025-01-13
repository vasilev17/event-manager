namespace EventManager.Services.Models.Event
{
    public class PaginationServiceModel
    {
        // public Guid LastEventId { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
