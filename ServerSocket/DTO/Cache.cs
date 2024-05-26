using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerSocket.DTO
{
    [BsonIgnoreExtraElements]
    public class Cache
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Key")]
        public string Key { get; set; }

        [BsonElement("Value")]
        public string Value { get; set; }
    }
}