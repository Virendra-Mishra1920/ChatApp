using ServerSocket.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared.Exceptions
{
    public class ValueValidationException : ICSException
    {
        public ValueValidationException(string message) : base(message)
        {
        }
    }
}