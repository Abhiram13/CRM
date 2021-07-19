using System;
using Models;
using CRM;
using MongoDB.Driver;
using Database;

namespace AuthenticationService {
   public partial class Authenticate {
      public class Login : DatabaseService<Employee> {
         private LoginRequest request;
         private Employee emp;
         public Login(LoginRequest req) : base(Table.employee) {
            request = req;
         }

         private Employee employee() {
            FilterDefinition<Employee> filter = this.builders.Eq("empid", request.id);
            return this.collection.Find(filter).ToList()[0];
         }

         private bool hashPassword() {
            string hashedPassword = HashPassword.compareHash(emp.salt, request.password);
            return hashedPassword == emp.password;
         }

         public ResponseBody<string> authenticate() {
            emp = this.employee();
            bool passwordCheck = this.hashPassword();

            ResponseBody<string> response = new ResponseBody<string>();

            if (emp == null) {
               response.body = "Employee does not exist";
               response.statusCode = 404;
               return response;
            }

            if (passwordCheck == false) {
               response.body = "password is incorrect";
               response.statusCode = 401;
               return response;
            }

            response.body = Text.Encode($"{request.id.ToString()}_{request.password.ToString()}");
            response.statusCode = 200;

            return response;
         }
      }
   }
}