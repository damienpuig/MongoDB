using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Mongo.Data
{
    public class Address
    {
        [BsonElement("street")]
        public string street { get; set; }

        
        [BsonElement("zip")]
        public int zip { get; set; }

        [BsonElement("city")]
        public string city { get; set; }

        [BsonElement("state")]
        public string state { get; set; }
    }
}
