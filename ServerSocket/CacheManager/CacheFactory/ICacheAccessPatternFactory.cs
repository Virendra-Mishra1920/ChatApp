using ServerSocket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.CacheManager.CacheFactory
{
    public interface ICacheAccessPatternFactory
    {
        CacheModel CacheWrite(CacheModel cache);
    }
}