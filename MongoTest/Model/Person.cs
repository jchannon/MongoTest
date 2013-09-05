using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTest.Model
{
    public class Person
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public long InternalId { get; set; }

        public string Name { get; set; }

        public int BatchNo { get; set; }
    }
}