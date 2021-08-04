using Models;
using System;
using Services.DatabaseManagement;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Collections.Generic;
using CRM;

namespace Services {
   namespace EmployeeManagement {
      public partial class EmployeeService : Services<Employee> {
         public Employee FetchById(int id) {
            FilterDefinition<Employee> filter = document.builders.Eq("empid", id);
            return document.FetchOne(filter);
         }
      }
   }
}