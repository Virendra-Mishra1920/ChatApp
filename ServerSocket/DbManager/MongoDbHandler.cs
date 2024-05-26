using MongoDB.Driver;
using ServerSocket.DTO;
using ServerSocket.Models;
using ServerSocket.Shared;
using ServerSocket.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.DbManager
{
    public class MongoDbHandler : IDataManager
    {
        //public static IMongoClient mongoClient { get; set; }
        //public static IMongoDatabase database { get; set; }

        public static string MongoConnection = "mongodb://abhijeetsharma16799:abhijeet99@cachedata-shard-00-00.1wq5i.mongodb.net:27017,cachedata-shard-00-01.1wq5i.mongodb.net:27017,cachedata-shard-00-02.1wq5i.mongodb.net:27017/Cache_db?ssl=true&replicaSet=atlas-4rzqsn-shard-0&authSource=admin&retryWrites=true&w=majority";

        private static MongoClient dbClient = new MongoClient(MongoConnection);
        private static IMongoDatabase database = dbClient.GetDatabase("Cache_db");
        private IMongoCollection<Cache> cacheDataCollection = database.GetCollection<Cache>("Start");

        private static Cache collection = new Cache();

        public CacheModel GetData(string key)
        {
            CacheModel cache = new CacheModel();
            try
            {
                var filter = Builders<Cache>.Filter.Eq("key", key);
                Cache cacheData = cacheDataCollection.Find(filter).FirstOrDefault();
                if (cacheData != null)
                {
                    cache.Key = key;
                    cache.Value = cacheData.Value;
                }
                else
                {
                    cache.Key = key;
                    cache.Value = string.Empty;
                    throw new DbKeyNotAvailableException(AppConstant.KeyNotAvailableInDb);
                }
            }
            catch (DbKeyNotAvailableException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return cache;
        }

        public void WriteToDB(CacheModel cache)
        {
            try
            {
                collection.Key = cache.Key;
                collection.Value = cache.Value;
                cacheDataCollection.InsertOne(collection);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}