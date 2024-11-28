namespace EventManager.Services.Exceptions
{
    public class EmailSenderException : Exception
    {
        public EmailSenderException() { }

        public EmailSenderException(string message) : base(message) { }
    }
}
