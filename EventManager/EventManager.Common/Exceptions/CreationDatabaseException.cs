namespace EventManager.Common.Exceptions
{
    public class CreationDatabaseException : Exception
    {
        public CreationDatabaseException() { }

        public CreationDatabaseException(string message) : base(message) { }
    }
}
