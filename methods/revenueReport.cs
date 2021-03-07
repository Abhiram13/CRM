﻿using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class RevenueData {
      public long LIFE { get; set; }
      public long GENERAL { get; set; }
      public long MUTUAL { get; set; }
      public long FIXED { get; set; }
      public long TOTAL { get; set; }
   }

   public class RevenueReport {
      public DateTime ENTRY_DATE { get; set; }
      public RevenueData DATA { get; set; }
   }

   public class RevenueReportTotal {
      public RevenueReport[] revenue { get; set; }
      public RevenueData total { get; set; }
   }

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

      private async Task<TransactionType[]> dateFilteredTransactions<TransactionType>(string transaction) {
         TransactionType[] transactions = await new Filter().Transactions<TransactionType, ZonalRevenueReport>(await Context, transaction);
         return transactions;
      }

      private async Task<DateTime[]> extractEntryDates() {
         List<DateTime> entryDates = new List<DateTime>();
         ILifeTransaction[] lifeTransactions = await dateFilteredTransactions<ILifeTransaction>("life_insurance");
         IGeneralInsurance[] generalTransactions = await dateFilteredTransactions<IGeneralInsurance>("general_insurance");
         IFixedDeposit[] fixedTransactions = await dateFilteredTransactions<IFixedDeposit>("fixed_deposit");
         IMutualFunds[] mutualTransactions = await dateFilteredTransactions<IMutualFunds>("mutual_funds");

         foreach (ILifeTransaction life in lifeTransactions) {
            if (entryDates.Contains(life.ENTRY_DATE) == false) {
               entryDates.Add(life.ENTRY_DATE);
            }
         }

         foreach (IGeneralInsurance general in generalTransactions) {
            if (entryDates.Contains(general.ENTRY_DATE) == false) {
               entryDates.Add(general.ENTRY_DATE);
            }
         }

         foreach (IFixedDeposit fixedDeposit in fixedTransactions) {
            if (entryDates.Contains(fixedDeposit.ENTRY_DATE) == false) {
               entryDates.Add(fixedDeposit.ENTRY_DATE);
            }
         }

         foreach (IMutualFunds mutual in mutualTransactions) {
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

      private async Task<RevenueData> total() {
         RevenueReport[] revenues = await fetchReportData();
         long life = 0;
         long general = 0;
         long fixedD = 0;
         long mutual = 0;
         long total = 0;

         for (int i = 0; i < revenues.Length; i++) {
            life += revenues[i].DATA.LIFE;
            general += revenues[i].DATA.GENERAL;
            fixedD += revenues[i].DATA.FIXED;
            mutual += revenues[i].DATA.MUTUAL;
            total += revenues[i].DATA.TOTAL;
         }

         return new RevenueData() {
            FIXED = fixedD,
            GENERAL = general,
            LIFE = life,
            MUTUAL = mutual,
            TOTAL = total
         };
      }

      private async Task<RevenueReport[]> fetchReportData() {
         DateTime[] dates = await extractEntryDates();
         List<RevenueReport> list = new List<RevenueReport>();

         for (int i = 0; i < dates.Length; i++) {
            long LIFE_REVENUE = revenues<ILifeTransaction>(dates[i], await dateFilteredTransactions<ILifeTransaction>("life_insurance"));
            long GENERAL_REVENUE = revenues<IGeneralInsurance>(dates[i], await dateFilteredTransactions<IGeneralInsurance>("general_insurance"));
            long FIXED_REVENUE = revenues<IFixedDeposit>(dates[i], await dateFilteredTransactions<IFixedDeposit>("fixed_deposit"));
            long MUTUAL_REVENUE = revenues<IMutualFunds>(dates[i], await dateFilteredTransactions<IMutualFunds>("mutual_funds"));

            list.Add(new RevenueReport() {
               ENTRY_DATE = dates[i],
               DATA = new RevenueData() {
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
         return Serialize<RevenueReportTotal>(
            new RevenueReportTotal() {
               revenue = await fetchReportData(),
               total = await total()
            }
         );
      }
   }
}
