using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;

namespace CRM {
   public static class GeneralInsurance {
      public async static Task<GeneralInsuranceBody[]> Report(CustomerModel[] customers, ZonalProduct report) {
         GeneralInsuranceBody[] transactions = await new Filter().Transactions<GeneralInsuranceBody, ZonalProduct>(report, "general_insurance");
         List<GeneralInsuranceZ> reports = new List<GeneralInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               GeneralInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new GeneralInsuranceZ() {
                     AADHAAR = (long)customer.AADHAAR,
                     INSURANCE_TYPE = transaction.INSURANCE_TYPE,
                     MANAGER = transaction.MANAGER,
                     PAYMENT_TERM = transaction.PAYMENT_TERM,
                     PRODUCT = transaction.PRODUCT,                     
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
                     REVENUE = transaction.REVENUE,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<GeneralInsuranceBody[]> BranchReport(CustomerModel[] customers, BranchProduct report) {
         GeneralInsuranceBody[] transactions = await new Filter().Transactions<GeneralInsuranceBody, BranchProduct>(report, "general_insurance");
         List<GeneralInsuranceZ> reports = new List<GeneralInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               GeneralInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new GeneralInsuranceZ() {
                     AADHAAR = (long)customer.AADHAAR,
                     INSURANCE_TYPE = transaction.INSURANCE_TYPE,
                     MANAGER = transaction.MANAGER,
                     PAYMENT_TERM = transaction.PAYMENT_TERM,
                     PRODUCT = transaction.PRODUCT,
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
                     REVENUE = transaction.REVENUE,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<GeneralInsuranceBody[]> RMReport(CustomerModel[] customers, RMProduct report) {
         GeneralInsuranceBody[] transactions = await new Filter().Transactions<GeneralInsuranceBody, RMProduct>(report, "general_insurance");
         List<GeneralInsuranceZ> reports = new List<GeneralInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            CustomerModel customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               GeneralInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
                  reports.Add(new GeneralInsuranceZ() {
                     AADHAAR = (long)customer.AADHAAR,
                     INSURANCE_TYPE = transaction.INSURANCE_TYPE,
                     MANAGER = transaction.MANAGER,
                     PAYMENT_TERM = transaction.PAYMENT_TERM,
                     PRODUCT = transaction.PRODUCT,
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
                     REVENUE = transaction.REVENUE,
                  });
               }
            }
         }

         return reports.ToArray();
      }
   }
}
