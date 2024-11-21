namespace EventManager.Common.Constants
{
    public static class ExceptionConstants
    {
        //User Validation Exceptions
        public const string InvalidEmailFormat = "Email: \"{0}\" is not in correct format!";
        public const string InvalidPasswordSize = "Password must at least 8 charecters long!";
        public const string PasswordSpecialCharecters = "Password must meet at least 3 of the requierments!";
        public const string AllPropertiesRequiered = "All properties for registration are needed!";

        //Database exceptions
        public const string CanNotCreate = "Can not create {0}!";
        public const string CantAddToRole = "Failed to add user to role!";
    }
}
