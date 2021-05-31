using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;

namespace EmployeeManagement {
   public sealed partial class EmployeeController {
      private Task<Employee> employee;

      public EmployeeController(HttpContext Context) : base(Context) {
         this.employee = JSONObject.Deserilise<Employee>(Context);
      }

      public async Task<string> Add() {
         bool isEmployeeExist = await EmployeeController.IsEmployeeExist(this.employee.Id);

         return Add<Employee>(isEmployeeExist, Table.employee, await this.employee);
      }
   }
}