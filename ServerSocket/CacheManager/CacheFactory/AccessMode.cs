using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.CacheManager.CacheFactory
{
    internal enum AccessMode
    {
        WriteThrough,
        WriteBack,
        WriteAround
    }
}