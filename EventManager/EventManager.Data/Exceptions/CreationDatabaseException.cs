namespace EventManager.Data.Exceptions
{
    public class CreationDatabaseException : DatabaseException
    {
        public CreationDatabaseException() { }

        public CreationDatabaseException(string message) : base(message) { }
    }
}
