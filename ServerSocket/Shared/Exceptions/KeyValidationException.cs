using ServerSocket.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared.Exceptions
{
    public class KeyValidationException : ICSException
    {
        public KeyValidationException(string message) : base(message)
        {
        }
    }
}