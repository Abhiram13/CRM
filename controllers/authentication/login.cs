using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM;
using Models;

namespace AuthenticationController {
   public partial class Authorise {
      public static List<Employee> findOne() {
         IMongoCollection<Employee> collection = new Database<Employee>(Table.employee).collection;
         var builder = Builders<Employee>.Filter;
         var filter = builder.Eq("ID", 123456) & builder.Eq("PASSWORD", "123");
         return collection.Find(filter).ToList();    
      }
   }
}