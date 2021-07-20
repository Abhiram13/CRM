using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;

namespace EmployeeManagement {
   // public sealed partial class EmployeeController : Controller {
   //    private Task<Employee> employee;

   //    public EmployeeController(HttpContext Context) {
   //       this.employee = JSONObject.Deserilise<Employee>(Context);
   //    }

   //    public async Task<string> Add() {
   //       DocumentVerification<Employee> details = new DocumentVerification<Employee>() {
   //          boolean = !(await EmployeeController.IsEmployeeExist(this.employee.Id)),
   //          table = Table.employee,
   //          document = await this.employee,
   //       };

   //       return Add<Employee>(details);
   //    }
   // }
}