using System;

namespace ServerSocket.Exceptions
{
    public class ICSException : Exception
    {
        public ICSException(string message) : base(message)
        {
        }
    }
}