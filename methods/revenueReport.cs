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

      private async Task<IEmployee[]> zonalManagers(IEmployee emply) {
         List<IEmployee> zonalmanagers = new List<IEmployee>();

         zonalmanagers.Add(emply);

         foreach (IEmployee employee in await Employees) {
            if (employee.ROLE == "Branch Manager") {
               zonalmanagers.Add(employee);
            }
         }

         return zonalmanagers.ToArray();
      }

      private IEmployee[] branchManagers(IEmployee emply) {
         List<IEmployee> branchmanagers = new List<IEmployee>();

         branchmanagers.Add(emply);

         return branchmanagers.ToArray();
      }

      private async Task<IEmployee[]> fetchEmployees() {
         IEmployee EMPLOYEE = await role();

         switch (EMPLOYEE.ROLE) {
            case "Branch Manager":
               return branchManagers(EMPLOYEE);
            case "Zonal Manager":
               return await zonalManagers(EMPLOYEE);
            default:
               return await allManagers();
         }
      }

      public async Task<string> f() {
         return Serialize<IEmployee[]>(await fetchEmployees());
      }

      private async Task<IEmployee> role() {
         ZonalRevenueReport request = await Context;
         IEmployee employee = new IEmployee();

         foreach (IEmployee emp in await Employees) {
            if (emp.ID == request.MANAGER) {
               employee = emp;
            }
         }

         return employee;
      }

      private async Task<ILifeTransaction[]> lifeInsuranceTransactions() {
         ILifeTransaction[] transactions = await new Filter().Transactions<ILifeTransaction, ZonalRevenueReport>(await Context, "life_insurance");
         return transactions;
      }

      private async Task<IGeneralInsurance[]> generalInsuranceTransactions() {
         IGeneralInsurance[] transactions = await new Filter().Transactions<IGeneralInsurance, ZonalRevenueReport>(await Context, "general_insurance");
         return transactions;
      }

      private async Task<IFixedDeposit[]> fixedDepositTransactions() {
         IFixedDeposit[] transactions = await new Filter().Transactions<IFixedDeposit, ZonalRevenueReport>(await Context, "fixed_deposit");
         return transactions;
      }

      private async Task<IMutualFunds[]> mutualfundsTransactions() {
         IMutualFunds[] transactions = await new Filter().Transactions<IMutualFunds, ZonalRevenueReport>(await Context, "mutual_funds");
         return transactions;
      }

      private async Task<DateTime[]> extractEntryDates() {
         List<DateTime> entryDates = new List<DateTime>();

         foreach (ILifeTransaction life in await lifeInsuranceTransactions()) {
            if (entryDates.Contains(life.ENTRY_DATE) == false) {
               entryDates.Add(life.ENTRY_DATE);
            }
         }

         foreach (IGeneralInsurance general in await generalInsuranceTransactions()) {
            if (entryDates.Contains(general.ENTRY_DATE) == false) {
               entryDates.Add(general.ENTRY_DATE);
            }
         }

         foreach (IFixedDeposit fixedDeposit in await fixedDepositTransactions()) {
            if (entryDates.Contains(fixedDeposit.ENTRY_DATE) == false) {
               entryDates.Add(fixedDeposit.ENTRY_DATE);
            }
         }

         foreach (IMutualFunds mutual in await mutualfundsTransactions()) {
            if (entryDates.Contains(mutual.ENTRY_DATE) == false) {
               entryDates.Add(mutual.ENTRY_DATE);
            }
         }

         return entryDates.ToArray();
      }
   }
}
