using System.Threading.Tasks;
using Models;
using CRM;
using System;
using System.Collections.Generic;
using AuthenticationController;

namespace EmployeeManagement {
   public sealed partial class EmployeeController : Controller {
      public static async Task<Employee[]> FetchAllEmployees() {
         return await FetchAll<Employee>(Table.employee);
      }
   }
}