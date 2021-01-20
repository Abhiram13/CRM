using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM {
   public class ILifeTransaction : IMongoObject {
      public long MOBILE { get; set; }
      public long AADHAAR { get; set; }
      public string ACCOUNT { get; set; }
      public string BANK { get; set; }
      public string PLAN { get; set; }
      public int TERM { get; set; }
      public string COMPANY { get; set; }
      public int PREMIUM_PAYMENT_TERM { get; set; }
      public long GROSS { get; set; }
      public long NET { get; set; }
      public long REVENUE { get; set; }
      public DateTime ENTRY_DATE { get; set; }
      public int MANAGER { get; set; }
   }   

   public static class LifeInsurance {
      public async static Task<ILifeTransaction[]> Report(ICustomer[] customers, Zonal report) {
         ILifeTransaction[] transactions = await new Filter().Transactions<ILifeTransaction, Zonal>(report, "life_insurance");
         List<ZonalReport> reports = new List<ZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               ILifeTransaction transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new ZonalReport() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
                     COMPANY = transaction.COMPANY,
                     EMAIL = customer.EMAIL,
                     ENTRY_DATE = transaction.ENTRY_DATE,
                     FIRSTNAME = customer.FIRSTNAME,
                     GROSS = transaction.GROSS,
                     LASTNAME = customer.LASTNAME,
                     LOCATION = customer.LOCATION,
                     MOBILE = (long)customer.MOBILE,
                     NET = transaction.NET,
                     PLAN = transaction.PLAN,
                     PREMIUM_PAYMENT_TERM = transaction.PREMIUM_PAYMENT_TERM,
                     REVENUE = transaction.REVENUE,
                     TERM = transaction.TERM,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<ILifeTransaction[]> BranchReport(ICustomer[] customers, IBranchBody report) {
         ILifeTransaction[] transactions = await new Filter().Transactions<ILifeTransaction, IBranchBody>(report, "life_insurance");
         List<ZonalReport> reports = new List<ZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               ILifeTransaction transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new ZonalReport() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
                     COMPANY = transaction.COMPANY,
                     EMAIL = customer.EMAIL,
                     ENTRY_DATE = transaction.ENTRY_DATE,
                     FIRSTNAME = customer.FIRSTNAME,
                     GROSS = transaction.GROSS,
                     LASTNAME = customer.LASTNAME,
                     LOCATION = customer.LOCATION,
                     MOBILE = (long)customer.MOBILE,
                     NET = transaction.NET,
                     PLAN = transaction.PLAN,
                     PREMIUM_PAYMENT_TERM = transaction.PREMIUM_PAYMENT_TERM,
                     REVENUE = transaction.REVENUE,
                     TERM = transaction.TERM,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<ILifeTransaction[]> RMReport(ICustomer[] customers, RMReportBody report) {
         ILifeTransaction[] transactions = await new Filter().Transactions<ILifeTransaction, RMReportBody>(report, "life_insurance");
         List<ZonalReport> reports = new List<ZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               ILifeTransaction transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
                  reports.Add(new ZonalReport() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
                     COMPANY = transaction.COMPANY,
                     EMAIL = customer.EMAIL,
                     ENTRY_DATE = transaction.ENTRY_DATE,
                     FIRSTNAME = customer.FIRSTNAME,
                     GROSS = transaction.GROSS,
                     LASTNAME = customer.LASTNAME,
                     LOCATION = customer.LOCATION,
                     MOBILE = (long)customer.MOBILE,
                     NET = transaction.NET,
                     PLAN = transaction.PLAN,
                     PREMIUM_PAYMENT_TERM = transaction.PREMIUM_PAYMENT_TERM,
                     REVENUE = transaction.REVENUE,
                     TERM = transaction.TERM,
                  });
               }
            }
         }

         return reports.ToArray();
      }
   }
}
