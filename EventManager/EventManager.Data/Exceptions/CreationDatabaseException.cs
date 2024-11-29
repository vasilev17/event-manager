namespace EventManager.Data.Exceptions
{
    public class CreationDatabaseException : DatabaseException
    {
        public CreationDatabaseException() { }

        public CreationDatabaseException(string message) : base(message) { }

        public CreationDatabaseException(string message, Exception ex) : base(message, ex) { }
    }
}
