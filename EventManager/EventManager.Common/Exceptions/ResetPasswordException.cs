namespace EventManager.Common.Exceptions
{
    public class ResetPasswordException : Exception
    {
        public ResetPasswordException() { }

        public ResetPasswordException(string message) : base(message) { }
    }
}
