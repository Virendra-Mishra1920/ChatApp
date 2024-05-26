using ServerSocket.DbManager;
using ServerSocket.Exceptions;
using ServerSocket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerSocket.CacheManager.CacheFactory
{
    public class WriteThrough : ICacheAccessPatternFactory
    {
        private IDataManager _dataManager;

        public WriteThrough()
        {
            _dataManager = new DataManager();
        }

        public void WriteToCache(CacheModel cache)
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
        }

        public void SetToDB(CacheModel cache)
        {
            _dataManager.WriteToDB(cache);
        }

        public CacheModel CacheWrite(CacheModel cache)
        {
            Parallel.Invoke(
                () => WriteToCache(cache),
                () => SetToDB(cache));
            return cache;
        }
    }
}