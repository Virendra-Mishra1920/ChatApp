using System;
using System.Collections.Generic;
using System.Text;

namespace ClientSocket.Models
{
    public class ICSModel
    {
        public string Key { get; set; }

        public string Value { get; set; }

        public string TypeOfOperation { get; set; }

        public string port { get; set; }

        public string IP { get; set; }

        public string status { get; set; }
    }
}