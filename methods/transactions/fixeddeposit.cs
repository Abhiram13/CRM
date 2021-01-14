using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace CRM {
   public class IFixedDeposit : IMongoObject {
      public string COMPANY { get; set; }
      public string PRODUCT { get; set; }
      public string SCHEMA { get; set; }
      public int TENOUR { get; set; }
      public long MOBILE { get; set; }
      public long AADHAAR { get; set; }
      public string ACCOUNT { get; set; }
      public string BANK { get; set; }
      public long AMOUNT { get; set; }
      public long REVENUE { get; set; }
      public DateTime ENTRY_DATE { get; set; }
      public DateTime ISSUANCE_DATE { get; set; }
      public int MANAGER { get; set; }
   }

   public class FixedZonalReport : IFixedDeposit {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public static class FixedDeposit {
      public async static Task<IFixedDeposit[]> Report(ICustomer[] customers, Zonal report) {
         IFixedDeposit[] transactions = await new Filter().Transactions<IFixedDeposit, Zonal>(report, "fixed_deposit");
         List<FixedZonalReport> reports = new List<FixedZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IFixedDeposit transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new FixedZonalReport() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     AMOUNT = transaction.AMOUNT,
                     SCHEMA = transaction.SCHEMA,
                     TENOUR = transaction.TENOUR,
                     MANAGER = transaction.MANAGER,
                     PRODUCT = transaction.PRODUCT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
                     COMPANY = transaction.COMPANY,
                     EMAIL = customer.EMAIL,
                     ENTRY_DATE = transaction.ENTRY_DATE,
                     FIRSTNAME = customer.FIRSTNAME,
                     LASTNAME = customer.LASTNAME,
                     LOCATION = customer.LOCATION,
                     MOBILE = (long)customer.MOBILE,
                     REVENUE = transaction.REVENUE,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<IFixedDeposit[]> BranchReport(ICustomer[] customers, IBranchBody report) {
         IFixedDeposit[] transactions = await new Filter().Transactions<IFixedDeposit, IBranchBody>(report, "fixed_deposit");
         List<FixedZonalReport> reports = new List<FixedZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IFixedDeposit transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new FixedZonalReport() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     AMOUNT = transaction.AMOUNT,
                     SCHEMA = transaction.SCHEMA,
                     TENOUR = transaction.TENOUR,
                     MANAGER = transaction.MANAGER,
                     PRODUCT = transaction.PRODUCT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
                     COMPANY = transaction.COMPANY,
                     EMAIL = customer.EMAIL,
                     ENTRY_DATE = transaction.ENTRY_DATE,
                     FIRSTNAME = customer.FIRSTNAME,
                     LASTNAME = customer.LASTNAME,
                     LOCATION = customer.LOCATION,
                     MOBILE = (long)customer.MOBILE,
                     REVENUE = transaction.REVENUE,
                  });
               }
            }
         }

         return reports.ToArray();
      }
   }
}
