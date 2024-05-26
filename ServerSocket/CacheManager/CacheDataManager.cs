using ServerSocket.CacheManager.CacheFactory;
using ServerSocket.DbManager;
using ServerSocket.Models;
using ServerSocket.ServerConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket.CacheManager
{
    public class CacheDataManager
    {
        //private static readonly object obj = new Object();
        //private static CacheDataManager instance = null;

        //public static CacheDataManager GetInstance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new CacheDataManager(new DataManager());
        //        }
        //        return instance;
        //    }
        //}

        private DataManager _dataManager;

        public CacheDataManager()
        {
            _dataManager = new DataManager();
        }

        public CacheModel ManageCache(CacheModel cache, Socket clientSocket)
        {
            if (cache.TypeOfOperation == "GET")
            {
                cache = GetDataFromCache(cache, clientSocket);
                return cache;
            }
            else
            {
                return WriteToCache(cache, clientSocket);
            }
        }

        public CacheModel GetDataFromCache(CacheModel cache, Socket clientSocket)
        {
            cache = LruCache.GetCache(cache);
            if (String.IsNullOrEmpty(cache.Value))
            {
                cache = this._dataManager.GetData(cache.Key);
                if (String.IsNullOrEmpty(cache.Value))
                {
                    return cache;
                }
                return WriteToCache(cache, clientSocket, "WriteAround");
            }
            else
            {
                return cache;
            }
        }

        public CacheModel WriteToCache(CacheModel cache, Socket clientSocket, string type = null)
        {
            lock (clientSocket)
            {
                ICacheAccessPatternFactory objCache;
                if (type == null)
                {
                    //Access mode can be changed according to developer
                    objCache = CacheAccessPatternFactory.GetPolicyInstance(AccessMode.WriteBack);
                }
                else
                {
                    objCache = CacheAccessPatternFactory.GetPolicyInstance(AccessMode.WriteAround);
                }
                if (objCache == null)
                {
                    return cache;
                }
                return objCache.CacheWrite(cache);
            }
        }
    }
}