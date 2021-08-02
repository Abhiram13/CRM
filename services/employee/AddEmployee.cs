using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using AuthenticationService;
using DatabaseManagement;

namespace EmployeeManagement {
   public partial class EmployeeServ : Services<Employee> {
      public EmployeeServ(HttpRequest request) : base(request, Table.employee) {}

      public void Insert() {
         FilterDefinition<Employee> filter = document.builders.Eq("empid", requestBody.empid);
         HashDetails hash = HashPassword.hash(requestBody.password);
         requestBody.salt = hash.salt;
         requestBody.password = hash.password;         
         document.Insert(requestBody, filter);
      }
   }

   public sealed partial class EmployeeService {

      public async static void Insert(HttpRequest request) {
         Employee employee = await JSON.httpContextDeseriliser<Employee>(request);
         Document<Employee> document = new Document<Employee>(Table.employee);
         FilterDefinition<Employee> filter = document.builders.Eq("empid", employee.empid);
         new Document<Employee>(Table.employee).Insert(employee, filter);
         return;
      }

      public async static Task<ResponseBody<string>> Add(HttpRequest request) {
         try {
            Employee employee = await JSON.httpContextDeseriliser<Employee>(request);

            if (isEmployeeExist(employee.empid)) {
               HashDetails hash = HashPassword.hash(employee.password);
               employee.salt = hash.salt;
               employee.password = hash.password;
               Database<Employee> db = new Database<Employee>(Table.employee);
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