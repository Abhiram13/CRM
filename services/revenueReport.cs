using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;
using Models.RevenueReport.Reports;
using Models.RevenueReport.Zonal;
using Models.TransactionsRequestBody;

namespace CRM { 
   public class RevenueReports : JSON {
      private Task<ReportRequestBody> Context;
      private Task<Employee[]> Employees;
      public RevenueReports(HttpContext context) {
         Context = REPORT(context);
         Employees = allManagers();
      }

      private async Task<ReportRequestBody> REPORT(HttpContext context) {
         return await Deserilise<ReportRequestBody>(context);
      }

      private async Task<Employee[]> allManagers() {
         string employee = await new Database<Employee>("employee").FetchAll();
         return DeserializeObject<Employee[]>(employee);
      }

      private async Task<Employee[]> zonalManagers(Employee emply) {
         List<Employee> zonalmanagers = new List<Employee>();

         zonalmanagers.Add(emply);

         foreach (Employee employee in await Employees) {
            if (employee.ROLE == "Branch Manager") {
               zonalmanagers.Add(employee);
            }
         }

         return zonalmanagers.ToArray();
      }

      private Employee[] branchManagers(Employee emply) {
         List<Employee> branchmanagers = new List<Employee>();

         branchmanagers.Add(emply);

         return branchmanagers.ToArray();
      }

      private async Task<Employee[]> fetchEmployees() {
         Employee EMPLOYEE = await role();

         switch (EMPLOYEE.ROLE) {
            case "Branch Manager":
               return branchManagers(EMPLOYEE);
            case "Zonal Manager":
               return await zonalManagers(EMPLOYEE);
            default:
               return await allManagers();
         }
      }

      private async Task<Employee> role() {
         ReportRequestBody request = await Context;
         Employee employee = new Employee();

         foreach (Employee emp in await Employees) {
            if (emp.ID == request.MANAGER) {
               employee = emp;
            }
         }

         return employee;
      }

      private async Task<TransactionType[]> dateFilteredTransactions<TransactionType>(string transaction) {
         TransactionType[] transactions = await new Filter().Transactions<TransactionType, ReportRequestBody>(await Context, transaction);
         return transactions;
      }

      private async Task<DateTime[]> extractEntryDates() {
         List<DateTime> entryDates = new List<DateTime>();
         LifeInsuranceBody[] lifeTransactions = await dateFilteredTransactions<LifeInsuranceBody>("life_insurance");
         GeneralInsuranceBody[] generalTransactions = await dateFilteredTransactions<GeneralInsuranceBody>("general_insurance");
         FixedDepositBody[] fixedTransactions = await dateFilteredTransactions<FixedDepositBody>("fixed_deposit");
         MutualFundsBody[] mutualTransactions = await dateFilteredTransactions<MutualFundsBody>("mutual_funds");

         foreach (LifeInsuranceBody life in lifeTransactions) {
            if (entryDates.Contains(life.ENTRY_DATE) == false) {
               entryDates.Add(life.ENTRY_DATE);
            }
         }

         foreach (GeneralInsuranceBody general in generalTransactions) {
            if (entryDates.Contains(general.ENTRY_DATE) == false) {
               entryDates.Add(general.ENTRY_DATE);
            }
         }

         foreach (FixedDepositBody fixedDeposit in fixedTransactions) {
            if (entryDates.Contains(fixedDeposit.ENTRY_DATE) == false) {
               entryDates.Add(fixedDeposit.ENTRY_DATE);
            }
         }

         foreach (MutualFundsBody mutual in mutualTransactions) {
            if (entryDates.Contains(mutual.ENTRY_DATE) == false) {
               entryDates.Add(mutual.ENTRY_DATE);
            }
         }

         return entryDates.ToArray();
      }

      private long revenues<TransactionType>(DateTime date, TransactionType[] transactions) {
         long revenue = 0;

         for (int i = 0; i < transactions.Length; i++) {
            if ((DateTime)typeof(TransactionType).GetProperty("ENTRY_DATE").GetValue(transactions[i]) == date) {
               revenue += (long)typeof(TransactionType).GetProperty("REVENUE").GetValue(transactions[i]);
            }
         }

         return revenue;
      }

      private async Task<DataModel> total() {
         ReportModel[] revenues = await fetchReportData();
         long life = 0, general = 0, fixedD = 0, mutual = 0, total = 0;

         for (int i = 0; i < revenues.Length; i++) {
            life += revenues[i].DATA.LIFE;
            general += revenues[i].DATA.GENERAL;
            fixedD += revenues[i].DATA.FIXED;
            mutual += revenues[i].DATA.MUTUAL;
            total += revenues[i].DATA.TOTAL;
         }

         return new DataModel() {
            FIXED = fixedD,
            GENERAL = general,
            LIFE = life,
            MUTUAL = mutual,
            TOTAL = total
         };
      }

      private async Task<ReportModel[]> fetchReportData() {
         DateTime[] dates = await extractEntryDates();
         List<ReportModel> list = new List<ReportModel>();

         for (int i = 0; i < dates.Length; i++) {
            long LIFE_REVENUE = revenues<LifeInsuranceBody>(dates[i], await dateFilteredTransactions<LifeInsuranceBody>("life_insurance"));
            long GENERAL_REVENUE = revenues<GeneralInsuranceBody>(dates[i], await dateFilteredTransactions<GeneralInsuranceBody>("general_insurance"));
            long FIXED_REVENUE = revenues<FixedDepositBody>(dates[i], await dateFilteredTransactions<FixedDepositBody>("fixed_deposit"));
            long MUTUAL_REVENUE = revenues<MutualFundsBody>(dates[i], await dateFilteredTransactions<MutualFundsBody>("mutual_funds"));

            list.Add(new ReportModel() {
               ENTRY_DATE = dates[i],
               DATA = new DataModel() {
                  LIFE = LIFE_REVENUE,
                  GENERAL = GENERAL_REVENUE,
                  MUTUAL = MUTUAL_REVENUE,
                  FIXED = FIXED_REVENUE,
                  TOTAL = LIFE_REVENUE + GENERAL_REVENUE + MUTUAL_REVENUE + FIXED_REVENUE
               },
            });
         }

         return list.ToArray();
      }

      public async Task<string> report() {
         return Serialize<TotalModel>(
            new TotalModel() {
               revenue = await fetchReportData(),
               total = await total()
            }
         );
      }
   }
}
