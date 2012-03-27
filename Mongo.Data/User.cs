using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Mongo.Data
{
    public class User
    {
        [BsonId]
        public ObjectId  _id { get; set; }

        [BsonElement("name")]
        public string name { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("address")]
        public Address addresse { get; set; }

        [BsonIgnoreIfNull]
        [BsonElement("Properties")]
        public Dictionary<string, object> Properties { get; set; }
    }
}
