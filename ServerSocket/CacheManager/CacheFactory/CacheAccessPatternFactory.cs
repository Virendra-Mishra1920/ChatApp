using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.CacheManager.CacheFactory
{
    internal class CacheAccessPatternFactory
    {
        public static ICacheAccessPatternFactory GetPolicyInstance(AccessMode policy)
        {
            switch (policy)
            {
                case AccessMode.WriteAround:
                    return new WriteAround();

                case AccessMode.WriteThrough:
                    return new WriteThrough();

                case AccessMode.WriteBack:
                    return new WriteBack();

                default:
                    return null;
            }
        }
    }
}