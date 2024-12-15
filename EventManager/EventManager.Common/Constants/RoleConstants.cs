namespace EventManager.Common.Constants
{
    public enum Roles
    {
        Admin,
        User,
        Organizer,
        Verified
    }

    public static class RoleConstants
    {
        public const Roles DefaultRole = Roles.User;
        public const string Creators = "Organizer, Admin";
        public const string Organizer = "Organizer";
        public const string Admin = "Admin";
    }
}
