using ServerSocket.Models;
using ServerSocket.Shared;
using ServerSocket.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace ServerSocket.CacheManager
{
    public class LruCache
    {
        public static LinkedList<KeyValuePair<string, string>> cacheList = new LinkedList<KeyValuePair<string, string>>();
        public static Dictionary<string, LinkedListNode<KeyValuePair<string, string>>> map = new Dictionary<string, LinkedListNode<KeyValuePair<string, string>>>();
        public static TimeSpan ExpiresAfter { get; } = TimeSpan.FromMinutes(1);
        public static Dictionary<string, DateTimeOffset> ExpirationTimeHandler = new Dictionary<string, DateTimeOffset>();

        //capacity is provided as 2 for testing purpose
        public static int capacity = 2;

        public static CacheModel GetCache(CacheModel cache)
        {
            RemoveCacheFromExpiration(cache.Key);
            try
            {
                if (!map.ContainsKey(cache.Key))
                {
                    throw new CacheKeyNotAvailableException(AppConstant.KeyNotAvailableInCache);
                }
                var node = map[cache.Key];
                cacheList.Remove(node);
                map[cache.Key] = cacheList.AddFirst(node.Value);
                cache.Value = node.Value.Value;
                return cache;
            }
            catch (CacheKeyNotAvailableException e)
            {
                Console.WriteLine("Warning" + e);
                return cache;
            }
        }

        public static void RemoveCacheFromExpiration(string key)
        {
            if (map.ContainsKey(key))
            {
                var expirationTime = LruCache.ExpirationTimeHandler[key];
                if (DateTimeOffset.Now - expirationTime >= LruCache.ExpiresAfter)
                {
                    var node = map[key];
                    cacheList.Remove(node);
                    map.Remove(key);
                }
            }
        }

        public static void AddExpiration(string key)
        {
            if (ExpirationTimeHandler.ContainsKey(key))
            {
                ExpirationTimeHandler[key] = DateTimeOffset.Now;
            }
            else
            {
                ExpirationTimeHandler.Add(key, DateTimeOffset.Now);
            }
        }
    }
}