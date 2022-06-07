﻿namespace DataAccess.Services.Extensions
{
    public class RequestedResourceNotFoundException : Exception
    {
        public RequestedResourceNotFoundException()
        {
        }

        public RequestedResourceNotFoundException(string message)
            : base(message)
        {
        }

        public RequestedResourceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
