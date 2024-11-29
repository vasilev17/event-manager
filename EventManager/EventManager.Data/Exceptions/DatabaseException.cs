namespace EventManager.Data.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() { }

        public DatabaseException(string message) : base(message) { }

        public DatabaseException(string message, Exception ex) : base(message, ex) { }
    }
}
