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
         Employees = allManagers();
      }

      private async Task<ZonalRevenueReport> REPORT(HttpContext context) {
         return await Deserilise<ZonalRevenueReport>(context);
      }

      private async Task<IEmployee[]> allManagers() {
         string employee = await new Database<IEmployee>("employee").FetchAll();
         return DeserializeObject<IEmployee[]>(employee);
      }

      private async Task<IEmployee[]> zonalManagers() {
         List<IEmployee> zonalmanagers = new List<IEmployee>();

         foreach (IEmployee employee in await Employees) {
            if (employee.ROLE == "Zonal Manager") {
               zonalmanagers.Add(employee);
            }
         }

         return zonalmanagers.ToArray();
      }

      private async Task<IEmployee[]> branchManagers() {
         List<IEmployee> branchmanagers = new List<IEmployee>();

         foreach (IEmployee employee in await Employees) {
            if (employee.ROLE == "Branch Manager") {
               branchmanagers.Add(employee);
            }
         }

         return branchmanagers.ToArray();
      }

      private async Task<IEmployee[]> fetchEmployees() {
         string ROLE = await role();

         switch (ROLE) {
            case "Branch Manager":
               return await branchManagers();
            case "Zonal Manager":
               return await zonalManagers();
            default:
               return await allManagers();
         }
      }

      public async Task<string> f() {
         return Serialize<IEmployee[]>(await fetchEmployees());
      }

      private async Task<string> role() {
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
