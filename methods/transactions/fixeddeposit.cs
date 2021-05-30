using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;

namespace CRM {
   public static class FixedDeposit {
      public async static Task<FixedDepositBody[]> Report(Customer[] customers, ZonalProduct report) {
         List<FixedDepositBody> transactions = await Transactions.FetchFromDateRange<FixedDepositBody, ZonalProduct>(report, Table.fixedDeposit);
         List<FixedDepositZ> reports = new List<FixedDepositZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Count; tIndex++) {
               FixedDepositBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new FixedDepositZ() {
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

      public async static Task<FixedDepositBody[]> BranchReport(Customer[] customers, BranchProduct report) {
         FixedDepositBody[] transactions = await new Filter().Transactions<FixedDepositBody, BranchProduct>(report, "fixed_deposit");
         List<FixedDepositZ> reports = new List<FixedDepositZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               FixedDepositBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new FixedDepositZ() {
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

      public async static Task<FixedDepositBody[]> RMReport(Customer[] customers, RMProduct report) {
         FixedDepositBody[] transactions = await new Filter().Transactions<FixedDepositBody, RMProduct>(report, "fixed_deposit");
         List<FixedDepositZ> reports = new List<FixedDepositZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               FixedDepositBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
                  reports.Add(new FixedDepositZ() {
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
