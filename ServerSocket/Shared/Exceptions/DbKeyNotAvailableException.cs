using ServerSocket.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared.Exceptions
{
    internal class DbKeyNotAvailableException : KeyNotAvailableException
    {
        public DbKeyNotAvailableException(string message) : base(message)
        {
        }
    }
}