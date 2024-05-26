using ServerSocket.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared.Exceptions
{
    public class MessageParsingException : ICSException
    {
        public MessageParsingException(string message) : base(message)
        {
        }
    }
}