using System;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Net;

namespace CRM {
   class Server {
      public static HttpListener http = new HttpListener();
      public static void start() {
         http.Prefixes.Add("http://localhost:2001/");
         http.Start();
         Console.WriteLine("Server had Started");
         while (true) {
            HttpListenerContext context = http.GetContext();
            switch (context.Request.RawUrl) {
               case "/":
                  new Response<string>(context).Send("Welcome to CRM");
                  break;
            }
         }
      }
   }
}