using Models;
using System;
using Database;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ApiManagement {
   public sealed partial class ApiServices {
      public static string[] fetchStates() {
         DatabaseService<States> db = new DatabaseService<States>(Table.states);
         List<States> states = db.collection.Find(new BsonDocument()).ToList();
         return states[0].states;
      }
   }
}