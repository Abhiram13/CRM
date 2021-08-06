using Models;
using System;
using Services.DatabaseManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using CRM;

namespace Services {
   namespace ApiManagement {
      public sealed class Locations : Document<LocationModel> {         
         private Locations() : base(Table.location) {}
         public static string[] fetchLocations() {
				List<LocationModel> locations = new Locations().FetchAll();
				List<string> list = new List<string>();

            foreach (LocationModel location in locations) {
               list.Add(location.location);
            }

            return list.ToArray();
         }
      }
   }
}