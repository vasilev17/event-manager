namespace EventManager.Common.Constants
{
    public enum Roles
    {
        Admin,
        User,
        Organizer
    }

    public static class RoleConstants
    {
        public const Roles DefaultRole = Roles.User;
    }
}
