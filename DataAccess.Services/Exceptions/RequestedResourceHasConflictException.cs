namespace DataAccess.Services.Exceptions
{
    public class RequestedResourceHasConflictException : Exception
    {
        public RequestedResourceHasConflictException()
        {
        }

        public RequestedResourceHasConflictException(string message)
            : base(message)
        {
        }

        public RequestedResourceHasConflictException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
