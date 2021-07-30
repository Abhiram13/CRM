using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;
using Microsoft.AspNetCore.Http;

namespace CRM {   
   public static class LifeInsurance {
      public async static Task<LifeInsuranceBody[]> Report(Customer[] customers, ZonalProduct report) {
         LifeInsuranceBody[] transactions = await new Filter().Transactions<LifeInsuranceBody, ZonalProduct>(report, "life_insurance");
         List<LifeInsuranceZ> reports = new List<LifeInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               LifeInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location) {
                  reports.Add(new LifeInsuranceZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     company = transaction.company,
                     email = customer.email,
                     entry_date = transaction.entry_date,
                     firstname = customer.firstname,
                     gross = transaction.gross,
                     lastname = customer.lastname,
                     location = customer.location,
                     mobile = (long)customer.mobile,
                     net = transaction.net,
                     plan = transaction.plan,
                     payment_term = transaction.payment_term,
                     revenue = transaction.revenue,
                     term = transaction.term,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<LifeInsuranceBody[]> BranchReport(Customer[] customers, BranchProduct report) {
         LifeInsuranceBody[] transactions = await new Filter().Transactions<LifeInsuranceBody, BranchProduct>(report, "life_insurance");
         List<LifeInsuranceZ> reports = new List<LifeInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               LifeInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location && customer.branch == report.branch) {
                  reports.Add(new LifeInsuranceZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     company = transaction.company,
                     email = customer.email,
                     entry_date = transaction.entry_date,
                     firstname = customer.firstname,
                     gross = transaction.gross,
                     lastname = customer.lastname,
                     location = customer.location,
                     mobile = (long)customer.mobile,
                     net = transaction.net,
                     plan = transaction.plan,
                     payment_term = transaction.payment_term,
                     revenue = transaction.revenue,
                     term = transaction.term,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<LifeInsuranceBody[]> RMReport(Customer[] customers, Rmproduct report) {
         LifeInsuranceBody[] transactions = await new Filter().Transactions<LifeInsuranceBody, Rmproduct>(report, "life_insurance");
         List<LifeInsuranceZ> reports = new List<LifeInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               LifeInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (report.manager == transaction.manager) {
                  reports.Add(new LifeInsuranceZ() {
                     aadhaar = (long)customer.aadhaar,
                     account = transaction.account,
                     bank = transaction.bank,
                     birthdate = (DateTime)customer.birthdate,
                     branch = customer.branch,
                     company = transaction.company,
                     email = customer.email,
                     entry_date = transaction.entry_date,
                     firstname = customer.firstname,
                     gross = transaction.gross,
                     lastname = customer.lastname,
                     location = customer.location,
                     mobile = (long)customer.mobile,
                     net = transaction.net,
                     plan = transaction.plan,
                     payment_term = transaction.payment_term,
                     revenue = transaction.revenue,
                     term = transaction.term,
                     manager = transaction.manager,
                  });
               }
            }
         }

         return reports.ToArray();
      }
   }

   public class LifeInsuranceTransactions {
      private HttpContext context;
      private Task<LifeInsuranceBody> lifeInsurance;

      public LifeInsuranceTransactions(HttpContext Context) {
         this.context = Context;
         this.lifeInsurance = this.deseriliseContext();
      }

      private async Task<LifeInsuranceBody> deseriliseContext() {
         return await JSONObject.Deserilise<LifeInsuranceBody>(this.context);
      }

      // private async Task<bool> isCustomerExist() {
      //    LifeInsuranceBody transaction = await lifeInsurance;
      //    return Customer.isCustomerExist(transaction.mobile, transaction.aadhaar) == null;
      // }
   }
}
