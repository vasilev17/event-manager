namespace EventManager.Data.Models.Picture
{
    public class Picture
    {
        public Guid Id { get; set; }

        public string? Url { get; set; }

        public string? ResourceType { get; set; }

        public string? PublicId { get; set; }
    }
}
