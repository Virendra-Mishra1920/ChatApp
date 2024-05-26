using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSocket.MessageValidator
{
    public class Validator : IValidator
    {
        public Validator()
        {

        }
        public bool ValidateKey(string key)
        {
            if (System.Text.ASCIIEncoding.ASCII.GetByteCount(key) > 250)
            {
                return false;
            }
            return true;
        }

        public bool ValidateValue(string value)
        {
            if (System.Text.ASCIIEncoding.ASCII.GetByteCount(value) > 1048576)
            {
                return false;
            }
            return true;
        }
    }
}