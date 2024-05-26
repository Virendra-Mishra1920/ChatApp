using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSocket.MessageValidator
{
    internal interface IValidator
    {
        public bool ValidateKey(string key);

        public bool ValidateValue(string value);
    }
}