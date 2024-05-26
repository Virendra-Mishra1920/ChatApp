using ServerSocket.DbManager;
using ServerSocket.Exceptions;
using ServerSocket.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.CacheManager.CacheFactory
{
    public class WriteBack : ICacheAccessPatternFactory
    {
        private IDataManager _dataManager;

        public WriteBack()
        {
            _dataManager = new DataManager();
        }

        public CacheModel CacheWrite(CacheModel cache)
        {
            try
            {
                LruCache.RemoveCacheFromExpiration(cache.Key);
                if (LruCache.map.ContainsKey(cache.Key))
                {
                    var node = LruCache.map[cache.Key];
                    LruCache.cacheList.Remove(node);
                    LruCache.map[cache.Key] = LruCache.cacheList.AddFirst(new KeyValuePair<string, string>(cache.Key, cache.Value));
                    LruCache.AddExpiration(cache.Key);
                }
                else
                {
                    if (LruCache.cacheList.Count >= LruCache.capacity)
                    {
                        string keyToBeRemoved = LruCache.cacheList.Last.Value.Key;
                        string valueToBeRemoved = LruCache.cacheList.Last.Value.Value;
                        SetToDb(keyToBeRemoved, valueToBeRemoved);
                        LruCache.map.Remove(keyToBeRemoved);
                        LruCache.cacheList.RemoveLast();
                    }
                    LruCache.map[cache.Key] = LruCache.cacheList.AddFirst(new KeyValuePair<string, string>(cache.Key, cache.Value));
                    LruCache.AddExpiration(cache.Key);
                }
            }
            catch (CacheWriterException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return cache;
        }

        public void SetToDb(string key, string value)
        {
            CacheModel cacheToBeWrittenInDataBase = new CacheModel();
            cacheToBeWrittenInDataBase.Key = key;
            cacheToBeWrittenInDataBase.Value = value;
            _dataManager.WriteToDB(cacheToBeWrittenInDataBase);
        }
    }
}