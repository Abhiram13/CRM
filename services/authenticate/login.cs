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

         public ResponseBody<string> authenticate() {
            bool isEmployee = this.isEmployee();
            bool passwordCheck = this.passwordCheck();

            Console.WriteLine(isEmployee);
            Console.WriteLine(passwordCheck);

            ResponseBody<string> response = new ResponseBody<string>();

            if (isEmployee == false) {
               response.body = "Employee does not exist";
               response.statusCode = 404;
            } else if (isEmployee == false && passwordCheck == false) {
               response.body = "password is incorrect";
               response.statusCode = 401;
            }

            response.body = "Authorise";
            response.statusCode = 200;

            return response;
         }
      }
   }
}