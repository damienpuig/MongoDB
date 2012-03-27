using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Threading.Tasks;
using Mongo.DB;
using System.Threading;

namespace Mongo
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Factory.StartNew(() =>
            {
                MongoProcess.StartMongoDB();
               
            });

            Thread.Sleep(10000);
            
           Task.Factory.StartNew(() =>
               {
                   var form = new NewUser();
                   form.ShowDialog();
               });


           Console.ReadLine();
        }

        
    }
}
