using ServerSocket.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared.Exceptions
{
    public class ConnectionNotFoundException : ICSException
    {
        public ConnectionNotFoundException(string message) : base(message)
        {
        }
    }
}