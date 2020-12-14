using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Net;

namespace CRM
{
   public static class Mongo
   {
      public static string url = "";
      public static MongoClient client = new MongoClient($"mongodb+srv://abhiramdb:{Config.password}@crm-cluster.i47fm.mongodb.net/{Config.db}?retryWrites=true&w=majority");
      public static IMongoDatabase database = client.GetDatabase(Config.db);
   }

   class Server
   {
      public static HttpListener http = new HttpListener();
      public static void start()
      {
         http.Prefixes.Add("http://localhost:2001/");
         http.Start();
         Console.WriteLine("Server had Started");
         try
         {
            while (true)
            {
               HttpListenerContext context = http.GetContext();
               switch (context.Request.RawUrl)
               {
                  case "/":
                     Console.WriteLine(
                        new Document<IEmployee>("employee").FetchAll()
                     );
                     Console.WriteLine("asvasgh");
                     new Response<string>(context).Send("Welcome to CRM");
                     break;
               }
            }
         }
         catch (Exception e)
         {
            Console.WriteLine(e.Message);
         }
      }
   }
}