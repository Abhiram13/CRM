using Models;
using System;
using Services.DatabaseManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using CRM;

/// FOR ADDING DOCUMENT IN DATABASE
// Take http request as parameter from route controller
// pass generic type of request object to the database model
// decode the request from seperate source/ class
// pass filter variables to insert document method to verify for existing documents
// pass response code back 

/// FOR FETCHING ALL DOCUMENTS FROM DATABASE
// pass generic parameter of document type to fetch all

namespace Services {
   namespace ApiManagement {
      public sealed class Locations : Document<Location> {         
         private Locations() : base(Table.location) {}
         public void Insert() {
            // new Locations().builders.Eq("location", )
         }

         public static string[] fetchLocations() {
				List<Location> locations = new Locations().FetchAll();
				List<string> list = new List<string>();

            foreach (Location location in locations) {
               list.Add(location.location);
            }

            return list.ToArray();
         }
      }
   }
}