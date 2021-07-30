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
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               MutualFundsBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location) {
                  reports.Add(new MutualFundsZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     amc = transaction.amc,
                     fund = transaction.fund,
                     amount = transaction.amount,
                     mode = transaction.mode,
                     option = transaction.option,
                     plan = transaction.plan,
                     sub_option = transaction.sub_option,
                     manager = transaction.manager,
                     payment_term = transaction.payment_term,
                     product = transaction.product,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     email = customer.email,
                     entry_date = transaction.entry_date,
                     firstname = customer.firstname,
                     lastname = customer.lastname,
                     location = customer.location,
                     mobile = (long)customer.mobile,
                     revenue = transaction.revenue,
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
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               MutualFundsBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location && customer.branch == report.branch) {
                  reports.Add(new MutualFundsZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     amc = transaction.amc,
                     fund = transaction.fund,
                     amount = transaction.amount,
                     mode = transaction.mode,
                     option = transaction.option,
                     plan = transaction.plan,
                     sub_option = transaction.sub_option,
                     manager = transaction.manager,
                     payment_term = transaction.payment_term,
                     product = transaction.product,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     email = customer.email,
                     entry_date = transaction.entry_date,
                     firstname = customer.firstname,
                     lastname = customer.lastname,
                     location = customer.location,
                     mobile = (long)customer.mobile,
                     revenue = transaction.revenue,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<MutualFundsBody[]> RMReport(Customer[] customers, Rmproduct report) {
         MutualFundsBody[] transactions = await new Filter().Transactions<MutualFundsBody, Rmproduct>(report, "mutual_funds");
         List<MutualFundsZ> reports = new List<MutualFundsZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               MutualFundsBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (report.manager == transaction.manager) {
                  reports.Add(new MutualFundsZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     amc = transaction.amc,
                     fund = transaction.fund,
                     amount = transaction.amount,
                     mode = transaction.mode,
                     option = transaction.option,
                     plan = transaction.plan,
                     sub_option = transaction.sub_option,
                     manager = transaction.manager,
                     payment_term = transaction.payment_term,
                     product = transaction.product,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     email = customer.email,
                     entry_date = transaction.entry_date,
                     firstname = customer.firstname,
                     lastname = customer.lastname,
                     location = customer.location,
                     mobile = (long)customer.mobile,
                     revenue = transaction.revenue,
                  });
               }
            }
         }

         return reports.ToArray();
      }
   }
}
