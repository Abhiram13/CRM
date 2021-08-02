using Models;
using System;
using Database;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ApiManagement {
   public sealed partial class ApiServices {
      public static string[] fetchLocations() {
         DatabaseService<LocationModel> db = new DatabaseService<LocationModel>(Table.location);
         List<LocationModel> locations = db.collection.Find(new BsonDocument()).ToList();
         List<string> list = new List<string>();

         foreach (LocationModel location in locations) {
            list.Add(location.location);
         }

         return list.ToArray();
      }
   }
}