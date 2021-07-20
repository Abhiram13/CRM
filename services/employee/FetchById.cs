using Models;
using System;
using Database;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EmployeeManagement {
   public partial class EmployeeService {
      public static Employee FetchById(int id) {
         DatabaseService<Employee> db = new DatabaseService<Employee>(Table.employee);
         FilterDefinition<Employee> filter = db.builders.Eq("empid", id);
         var x = db.projection.Exclude("_id").Exclude("__v").Exclude("salt").Exclude("password");
         return db.collection.Find<Employee>(filter).Project<Employee>(x).ToList()[0];
      }
   }
}