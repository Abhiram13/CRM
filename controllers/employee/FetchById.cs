using System.Threading.Tasks;
using Models;
using CRM;
using System;

namespace EmployeeManagement {
   public sealed partial class EmployeeController {
      public static async Task<Employee> FetchById(string id) {
         Employee[] listOfEmployees = await EmployeeController.FetchAllEmployees();
         return Array.Find<Employee>(listOfEmployees, employee => employee.ID == Int32.Parse(id));
      }
   }
}