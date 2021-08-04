using System;
using Models;
using CRM;
using MongoDB.Driver;
using DatabaseManagement;
using System.Collections.Generic;

namespace AuthenticationService {
   public partial class Authenticate {
      public class Login : Database<Employee> {
         private LoginRequest request;         
         private Employee emp;
         public Login(LoginRequest req) : base(Table.employee) {
            request = req;
         }

         private Employee employee() {
            FilterDefinition<Employee> filter = this.builders.Eq("empid", request.id);
            List<Employee> list = this.collection.Find(filter).ToList();
            return list.Count > 0 ? list[0] : null;
         }

         private bool hashPassword() {
            string hashedPassword = HashPassword.compareHash(emp.salt, request.password);
            return hashedPassword == emp.password;
         }

         public ResponseBody<string> authenticate() {
            ResponseBody<string> response = new ResponseBody<string>();
            emp = this.employee();

            if (emp == null) {
               response.body = "Employee does not exist";               
               response.statusCode = 404;
               return response;
            }

            bool passwordCheck = this.hashPassword();            

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