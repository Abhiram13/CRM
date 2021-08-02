using System.Threading.Tasks;
using Models;
using CRM;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using DatabaseManagement;
using MongoDB.Driver;
using MongoDB.Bson;

namespace EmployeeManagement {
   public partial class EmployeeService {
      public static List<EmployeeResponseBody> FetchAllEmployees(HttpRequest request) {
         Database<Employee> db = new Database<Employee>(Table.employee);         
         List<Employee> employeesList = db.collection.Find<Employee>(new BsonDocument()).ToList();
         List<EmployeeResponseBody> response = new List<EmployeeResponseBody>();

         foreach (Employee employee in employeesList) {
            response.Add(new EmployeeResponseBody() { 
               branch = employee.branch,
               email = employee.email,
               empid = employee.empid,
               firstname = employee.firstname,
               lastname = employee.lastname,
               location = employee.location,
               mobile = employee.mobile,
               role = employee.role,
               state = employee.state,
               title = employee.title,            
            });
         }

         return response;
      }
   }
}