namespace EventManager.Common.Exceptions
{
    public class CredentialsAlreadyExistException : Exception
    {
        public CredentialsAlreadyExistException() { }

        public CredentialsAlreadyExistException(string message) : base(message) { }
    }
}
