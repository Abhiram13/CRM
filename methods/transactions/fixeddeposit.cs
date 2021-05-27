using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public static class FixedDeposit {
      public async static Task<IFixedDeposit[]> Report(CustomerModel[] customers, ZonalReportBody report) {
         IFixedDeposit[] transactions = await new Filter().Transactions<IFixedDeposit, ZonalReportBody>(report, "fixed_deposit");
         List<FixedZonalReport> reports = new List<FixedZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
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

      public async static Task<IFixedDeposit[]> BranchReport(ICustomer[] customers, BranchReportBody report) {
         IFixedDeposit[] transactions = await new Filter().Transactions<IFixedDeposit, BranchReportBody>(report, "fixed_deposit");
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

      public async static Task<IFixedDeposit[]> RMReport(ICustomer[] customers, RMReportBody report) {
         IFixedDeposit[] transactions = await new Filter().Transactions<IFixedDeposit, RMReportBody>(report, "fixed_deposit");
         List<FixedZonalReport> reports = new List<FixedZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IFixedDeposit transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
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
