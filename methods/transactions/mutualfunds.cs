using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;

namespace CRM {
   public static class MutualFunds {
      public async static Task<MutualFundsBody[]> Report(Customer[] customers, ZonalProduct report) {
         MutualFundsBody[] transactions = await new Filter().Transactions<MutualFundsBody, ZonalProduct>(report, "mutual_funds");
         List<MutualFundsZ> reports = new List<MutualFundsZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               MutualFundsBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new MutualFundsZ() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     AMC = transaction.AMC,
                     FUND = transaction.FUND,
                     AMOUNT = transaction.AMOUNT,
                     MODE = transaction.MODE,
                     OPTION = transaction.OPTION,
                     PLAN = transaction.PLAN,
                     SUB_OPTION = transaction.SUB_OPTION,
                     MANAGER = transaction.MANAGER,
                     PAYMENT_TERM = transaction.PAYMENT_TERM,
                     PRODUCT = transaction.PRODUCT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
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

      public async static Task<MutualFundsBody[]> BranchReport(Customer[] customers, BranchProduct report) {
         MutualFundsBody[] transactions = await new Filter().Transactions<MutualFundsBody, BranchProduct>(report, "mutual_funds");
         List<MutualFundsZ> reports = new List<MutualFundsZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               MutualFundsBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new MutualFundsZ() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     AMC = transaction.AMC,
                     FUND = transaction.FUND,
                     AMOUNT = transaction.AMOUNT,
                     MODE = transaction.MODE,
                     OPTION = transaction.OPTION,
                     PLAN = transaction.PLAN,
                     SUB_OPTION = transaction.SUB_OPTION,
                     MANAGER = transaction.MANAGER,
                     PAYMENT_TERM = transaction.PAYMENT_TERM,
                     PRODUCT = transaction.PRODUCT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
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

      public async static Task<MutualFundsBody[]> RMReport(Customer[] customers, RMProduct report) {
         MutualFundsBody[] transactions = await new Filter().Transactions<MutualFundsBody, RMProduct>(report, "mutual_funds");
         List<MutualFundsZ> reports = new List<MutualFundsZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               MutualFundsBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
                  reports.Add(new MutualFundsZ() {
                     AADHAAR = (long)customer.AADHAAR,
                     ACCOUNT = transaction.ACCOUNT,
                     AMC = transaction.AMC,
                     FUND = transaction.FUND,
                     AMOUNT = transaction.AMOUNT,
                     MODE = transaction.MODE,
                     OPTION = transaction.OPTION,
                     PLAN = transaction.PLAN,
                     SUB_OPTION = transaction.SUB_OPTION,
                     MANAGER = transaction.MANAGER,
                     PAYMENT_TERM = transaction.PAYMENT_TERM,
                     PRODUCT = transaction.PRODUCT,
                     BANK = transaction.BANK,
                     BIRTHDATE = (DateTime)customer.BIRTHDATE,
                     BRANCH = customer.BRANCH,
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
