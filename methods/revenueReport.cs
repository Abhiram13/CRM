using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class RevenueReports : JSON {
      private Task<ZonalRevenueReport> Context;
      private Task<IEmployee[]> Employees;
      public RevenueReports(HttpContext context) {
         Context = REPORT(context);
         Employees = fetchAllEmployees();
      }

      private async Task<ZonalRevenueReport> REPORT(HttpContext context) {
         return await Deserilise<ZonalRevenueReport>(context);
      }

      private async Task<IEmployee[]> fetchAllEmployees() {
         string employee = await new Database<IEmployee>("employee").FetchAll();
         return DeserializeObject<IEmployee[]>(employee);
      }

      public async Task<string> role() {
         ZonalRevenueReport request = await Context;
         string Role = "";

         foreach (IEmployee emp in await Employees) {
            if (emp.ID == request.MANAGER) {
               Role = emp.ROLE;
            }
         }

         return Role;
      }

      private void employeeById() {
         //IEmployee[] employeesList = await this.fetchAllEmployees();
         //IEmployee Employee = Array.Find<IEmployee>(employeesList, employee => employee.ID.ToString() == id);
         //return Serialize<IEmployee>(Employee);
      }
   }
}
