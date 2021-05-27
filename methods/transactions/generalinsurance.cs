using System;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM {
   public static class GeneralInsurance {
      public async static Task<IGeneralInsurance[]> Report(ICustomer[] customers, ZonalReportBody report) {
         IGeneralInsurance[] transactions = await new Filter().Transactions<IGeneralInsurance, ZonalReportBody>(report, "general_insurance");
         List<GeneralZonalReport> reports = new List<GeneralZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IGeneralInsurance transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new GeneralZonalReport() {
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

      public async static Task<IGeneralInsurance[]> BranchReport(ICustomer[] customers, BranchReportBody report) {
         IGeneralInsurance[] transactions = await new Filter().Transactions<IGeneralInsurance, BranchReportBody>(report, "general_insurance");
         List<GeneralZonalReport> reports = new List<GeneralZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IGeneralInsurance transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION && customer.BRANCH == report.BRANCH) {
                  reports.Add(new GeneralZonalReport() {
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

      public async static Task<IGeneralInsurance[]> RMReport(ICustomer[] customers, RMReportBody report) {
         IGeneralInsurance[] transactions = await new Filter().Transactions<IGeneralInsurance, RMReportBody>(report, "general_insurance");
         List<GeneralZonalReport> reports = new List<GeneralZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IGeneralInsurance transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (report.MANAGER == transaction.MANAGER) {
                  reports.Add(new GeneralZonalReport() {
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
