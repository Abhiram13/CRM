using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;
using TransactionManagement;

namespace CRM {
   public static class FixedDeposit {
      public async static Task<FixedDepositBody[]> Report(Customer[] customers, ZonalProduct report) {
         List<FixedDepositBody> transactions = await TransactionController.FetchFromDateRange<FixedDepositBody, ZonalProduct>(report, Table.fixedDeposit);
         List<FixedDepositZ> reports = new List<FixedDepositZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Count; tIndex++) {
               FixedDepositBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location) {
                  reports.Add(new FixedDepositZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     amount = transaction.amount,
                     schema = transaction.schema,
                     tenour = transaction.tenour,
                     manager = transaction.manager,
                     product = transaction.product,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     company = transaction.company,
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

      public async static Task<FixedDepositBody[]> BranchReport(Customer[] customers, BranchProduct report) {
         FixedDepositBody[] transactions = await new Filter().Transactions<FixedDepositBody, BranchProduct>(report, "fixed_deposit");
         List<FixedDepositZ> reports = new List<FixedDepositZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               FixedDepositBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location && customer.branch == report.branch) {
                  reports.Add(new FixedDepositZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     amount = transaction.amount,
                     schema = transaction.schema,
                     tenour = transaction.tenour,
                     manager = transaction.manager,
                     product = transaction.product,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     company = transaction.company,
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

      public async static Task<FixedDepositBody[]> RMReport(Customer[] customers, Rmproduct report) {
         FixedDepositBody[] transactions = await new Filter().Transactions<FixedDepositBody, Rmproduct>(report, "fixed_deposit");
         List<FixedDepositZ> reports = new List<FixedDepositZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               FixedDepositBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (report.manager == transaction.manager) {
                  reports.Add(new FixedDepositZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     amount = transaction.amount,
                     schema = transaction.schema,
                     tenour = transaction.tenour,
                     manager = transaction.manager,
                     product = transaction.product,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     company = transaction.company,
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
