namespace EventManager.Web.Models.User
{
    public class UserWebModel
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<string> Roles { get; set; }
    }
}
