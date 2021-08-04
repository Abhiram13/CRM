using Models;
using System;
using DatabaseManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Services {
   namespace ApiManagement {
      public sealed partial class Api {
         public static string[] fetchLocations() {
            Database<LocationModel> db = new Database<LocationModel>(Table.location);
            List<LocationModel> locations = db.collection.Find(new BsonDocument()).ToList();
            List<string> list = new List<string>();

            foreach (LocationModel location in locations) {
               list.Add(location.location);
            }

            return list.ToArray();
         }
      }
   }
}