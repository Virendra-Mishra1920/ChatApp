using ServerSocket.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared.Exceptions
{
    internal class CacheKeyNotAvailableException : KeyNotAvailableException
    {
        public CacheKeyNotAvailableException(string message) : base(message)
        {
        }
    }
}