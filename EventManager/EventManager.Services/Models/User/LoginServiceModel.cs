namespace EventManager.Services.Models.User
{
    public record LoginServiceModel
    {
        public required string Password { get; set; }

        public required string UserName { get; set; }
    }
}
