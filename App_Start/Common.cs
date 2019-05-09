using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc5mongocrud.App_Start
{
    public class Common
    {
        public class Base
        {
            public Base()
            {
                this._id = Guid.NewGuid().ToString();
            }
            [BsonElement("_id")]
            public string _id { get; set; }
        }

        public static IMongoDatabase conn;
        public static IMongoDatabase Connector
        {
            get
            {
                if (conn == null)
                {
                    MongoClient client = new MongoClient("mongodb://localhost:27017");
                    conn = client.GetDatabase("aful");
                    return conn;
                }
                return conn;
            }
            set { }
        }
    }
}