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
      public async static Task<LifeInsuranceBody[]> Report(CustomerModel[] customers, ZonalProduct report) {
         LifeInsuranceBody[] transactions = await new Filter().Transactions<LifeInsuranceBody, ZonalProduct>(report, "life_insurance");
         List<LifeInsuranceZ> reports = new List<LifeInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               LifeInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new LifeInsuranceZ() {
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

      public async static Task<LifeInsuranceBody[]> BranchReport(CustomerModel[] customers, BranchProduct report) {
         LifeInsuranceBody[] transactions = await new Filter().Transactions<LifeInsuranceBody, BranchProduct>(report, "life_insurance");
         List<LifeInsuranceZ> reports = new List<LifeInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               LifeInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new LifeInsuranceZ() {
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

      public async static Task<LifeInsuranceBody[]> RMReport(CustomerModel[] customers, RMProduct report) {
         LifeInsuranceBody[] transactions = await new Filter().Transactions<LifeInsuranceBody, RMProduct>(report, "life_insurance");
         List<LifeInsuranceZ> reports = new List<LifeInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               LifeInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
                  reports.Add(new LifeInsuranceZ() {
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
                     MANAGER = transaction.MANAGER,
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

      private async Task<bool> isCustomerExist() {
         LifeInsuranceBody transaction = await lifeInsurance;
         return Customer.isCustomerExist(transaction.MOBILE, transaction.AADHAAR) == null;
      }
   }
}
