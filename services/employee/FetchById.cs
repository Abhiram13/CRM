using Models;
using System;
using Database;
using MongoDB.Driver;

namespace EmployeeManagement {
   public partial class EmployeeService {
      public Employee FetchById(int id) {
         DatabaseService<Employee> db = new DatabaseService<Employee>(Table.employee);
         FilterDefinition<Employee> filter = db.builders.Eq("empid", id);
         return db.collection.Find(filter).ToList()[0];
      }
   }
}