using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Exceptions
{
    internal class CacheWriterException : ICSException
    {
        public CacheWriterException(string message) : base(message)
        {
        }
    }
}