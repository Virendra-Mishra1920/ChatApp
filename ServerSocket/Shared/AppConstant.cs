using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.Shared
{
    public class AppConstant
    {
        public static string KeyNotAvailableInCache = "Key Not Available In Cache";
        public static string KeyNotAvailableInDb = "Key Not Available In Database";
        public static string KeyValidationError = "Key Size is Larger than 250bytes";
        public static string ValueValidationError = "Value is Larger than 1mega bytes";
        public static string MessageParsingError = "Message not recived from Client";
    }
}