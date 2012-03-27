using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using MongoDB.Driver;
using System.Threading;

namespace Mongo.DB
{
   public class MongoProcess
    {

       internal const string MongoDBBinariesFolder = @"MongoDBBinaries";
       internal const string MongodLogFile = "mongod.log";
       internal const string TemplateLauncher = " --logpath {0}";
       internal const int Port = 27017;
       private  const string TemplateConnString = "mongodb://localhost:{0}";

       public static Process pDeamon = null;
       public static MongoServer server = null;
     

       private static bool CheckMongoDBRunning()
       {
           return !pDeamon.HasExited;
       }

        public static void StopMongoDB()
       {
           if (server != null)
           {
               server.Shutdown();
           }
       }

       private static void RunningMongoDB()
        {
            var mongoDisRunning = CheckMongoDBRunning();

            while (mongoDisRunning)
            {
                Logger.TraceInformation("MongoD running...");
                Thread.Sleep(3000);
                mongoDisRunning = CheckMongoDBRunning();
            }

                Logger.TraceInformation("MongoD stopped !");


        }

       public static void StartMongoDB()
       {

           var dApplication = Path.Combine(Path.Combine(Directory.GetCurrentDirectory(), MongoDBBinariesFolder), "mongod.exe");

           var commandLine = string.Format(TemplateLauncher, MongodLogFile);

           try
           {
               var deamonExist = Process.GetProcesses().ToList().Exists(p => p.ProcessName == "mongod");
               

               if (!deamonExist)
               {
                   
               
               pDeamon = new Process()
               {
                   StartInfo = new ProcessStartInfo(dApplication, commandLine)
                   {
                       UseShellExecute = true,
                       WorkingDirectory = MongoDBBinariesFolder,
                       CreateNoWindow = false,
                       WindowStyle = ProcessWindowStyle.Normal
                   }
               };

               pDeamon.Start();

               Logger.TraceInformation("deamon started..");

               }
               


           RunningMongoDB();
           server = MongoServer.Create(string.Format(TemplateConnString, Port)); 

           }
           catch (Exception exe)
           {
               Logger.TraceInformation(exe.Message);  
           }
       }


    }
}
