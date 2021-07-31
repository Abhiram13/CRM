using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using AuthenticationService;
using Database;

namespace EmployeeManagement {
   public sealed partial class EmployeeService {
      public async static Task<ResponseBody<string>> Add(HttpRequest request) {
         try {
            Employee employee = await JSONN.httpContextDeseriliser<Employee>(request);

            if (isEmployeeExist(employee.empid)) {
               HashDetails hash = HashPassword.hash(employee.password);
               employee.salt = hash.salt;
               employee.password = hash.password;
               DatabaseService<Employee> db = new DatabaseService<Employee>(Table.employee);
               db.collection.InsertOne(employee);

               return new ResponseBody<string>() {
                  body = "Employee added successfully",
                  statusCode = 200
               };
            }

            return new ResponseBody<string>() {
               body = "Employee already existed",
               statusCode = 302
            };

         } catch (Exception e) {
            return new ResponseBody<string>() {
               body = e.Message,
               statusCode = 500
            };
         }      
      }
   }
}