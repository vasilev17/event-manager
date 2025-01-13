namespace EventManager.Common.Exceptions
{
    public class InvalidRequestParametersException : Exception
    {
        public InvalidRequestParametersException() { }

        public InvalidRequestParametersException(string message) : base(message)
        {
        }
    }
}
