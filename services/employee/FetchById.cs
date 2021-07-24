using Models;
using System;
using Database;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;

namespace EmployeeManagement {
   public partial class EmployeeService {
      public static Employee FetchById(int id) {
         DatabaseService<Employee> db = new DatabaseService<Employee>(Table.employee);
         FilterDefinition<Employee> filter = db.builders.Eq("empid", id);
         ProjectionDefinition<Employee> projection = db.projection.Exclude("_id").Exclude("__v").Exclude("salt").Exclude("password");
         List<Employee> employeeList = db.collection.Find<Employee>(filter).Project<Employee>(projection).ToList();
         return employeeList.Count > 0 ? employeeList[0] : null;
      }
   }
}