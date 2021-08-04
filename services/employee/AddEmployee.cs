using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using AuthenticationService;
using DatabaseManagement;

namespace EmployeeManagement {
   public partial class EmployeeService : Services<Employee> {
      public EmployeeService(HttpRequest request) : base(request, Table.employee) {}

      public short Insert() {
         FilterDefinition<Employee> filter = document.builders.Eq("empid", requestBody.empid);
         HashDetails hash = HashPassword.hash(requestBody.password);
         requestBody.salt = hash.salt;
         requestBody.password = hash.password;
         return document.Insert(requestBody, filter);
      }
   }
}