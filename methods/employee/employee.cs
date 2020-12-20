using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM
{
   public class IEmployee
   {
      [BsonIgnoreIfNull]
      public MongoDB.Bson.ObjectId _id { get; set; } = new ObjectId();
      public int ID { get; set; } = 0;
      public string TITLE { get; set; } = "";
      public string FIRSTNAME { get; set; } = "";
      public string LASTNAME { get; set; } = "";
      public string EMAIL { get; set; } = "";
      public string PASSWORD { get; set; } = "";
      public int MOBILE { get; set; } = 0;
      public string GENDER { get; set; } = "";
      public string DESIGNATION { get; set; } = "";
      public string LOCATION { get; set; } = "";
      public string BRANCH { get; set; } = "";
      public string STATE { get; set; } = "";
      public string REGION { get; set; } = "";
      public string ROLE { get; set; } = "";
      public int? __v { get; } = 0;
   }

   public class Employee : JSON
   {
      private HttpContext context;
      public Employee(HttpContext Context)
      {
         context = Context;
      }

      public async void Check()
      {
         IEmployee emp = await Deserilise<IEmployee>(context);

         foreach (var key in emp.GetType().GetProperties())
         {
            Console.WriteLine(key.GetValue(emp));
            Console.WriteLine(key.GetType().BaseType);
         }
      }

      public void Add()
      {
         // new Database<IEmployee>("employee").Insert(employee);
      }
   }
}