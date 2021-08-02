using Models;
using System;
using Database;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ApiManagement {
   public sealed partial class ApiServices {
      public static string[] fetchRoles() {
         DatabaseService<Roles> db = new DatabaseService<Roles>(Table.roles);
         List<Roles> roles = db.collection.Find(new BsonDocument()).ToList();
         return roles[0].roles;
      }
   }
}