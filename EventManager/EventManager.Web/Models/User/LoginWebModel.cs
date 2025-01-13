namespace EventManager.Web.Models.User
{
    public record LoginWebModel
    {
        public required string Password { get; set; }

        public required string UserName { get; set; }
    }
}
