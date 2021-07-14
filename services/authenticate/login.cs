using System;
using Models;
using CRM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;
using System.Threading.Tasks;
using Database;

namespace AuthenticationService {
   public partial class Authenticate {
      public class Login : DatabaseService<Employee> {
         private LoginRequest request;
         public Login(LoginRequest req) : base(Table.employee) {
            request = req;
         }

         private bool isEmployee() {
            FilterDefinition<Employee> filter = this.builders.Eq("ID", request.id);
            return this.collection.Find(filter).ToList().Count > 0;
         }

         private bool passwordCheck() {
            FilterDefinition<Employee> filter = this.builders.Eq("ID", request.id) & this.builders.Eq("password", request.password);
            return this.collection.Find(filter).ToList().Count > 0;
         }

         public string authenticate() {
            bool isEmployee = this.isEmployee();
            bool passwordCheck = this.passwordCheck();

            if (isEmployee == false) {
               return "Employee does not exist";
            } else if (isEmployee == false && passwordCheck == false) {
               return "password is incorrect";
            }

            return "Authorise";
         }
      }
   }
}