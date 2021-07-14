using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Text.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using CRM;
using Models;

namespace AuthenticationController {
   public partial class Login {
      private LoginRequest request;
      public Login(LoginRequest req) {
         request = req;
      }

      private List<Employee> employeeId() {
         DB<Employee> db = new DB<Employee>(Table.employee);
         IMongoCollection<Employee> collection = db.collection;
         FilterDefinitionBuilder<Employee> builder = db.builders;
         FilterDefinition<Employee> filter = builder.Eq("ID", request.id);
         return collection.Find(filter).ToList();
      }

      private List<Employee> password() {
         DB<Employee> db = new DB<Employee>(Table.employee);
         IMongoCollection<Employee> collection = db.collection;
         FilterDefinitionBuilder<Employee> builder = db.builders;
         FilterDefinition<Employee> filter = builder.Eq("ID", request.id) & builder.Eq("PASSWORD", request.password);
         return collection.Find(filter).ToList();
      }

      public string authenticate() {
         int id = this.employeeId().Count;
         int password = this.password().Count;

         if (id > 0 && password > 0) {
            return "Ok";
         } else if (id == 0) {
            return "Employee does not exist";
         }

         return "password is incorrect";
      }
   }
}