using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Models;
using Models.ProductReportsRequestBody;
using Models.TransactionsRequestBody;
using Models.ZonalReportsResponseBody;

namespace CRM {
   public static class GeneralInsurance {
      public async static Task<GeneralInsuranceBody[]> Report(Customer[] customers, ZonalProduct report) {
         GeneralInsuranceBody[] transactions = await new Filter().Transactions<GeneralInsuranceBody, ZonalProduct>(report, "general_insurance");
         List<GeneralInsuranceZ> reports = new List<GeneralInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               GeneralInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location) {
                  reports.Add(new GeneralInsuranceZ() {
                     aadhaar = (long)customer.aadhaar,
                     insurance_type = transaction.insurance_type,
                     manager = transaction.manager,
                     payment_term = transaction.payment_term,
                     product = transaction.product,                     
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
                     revenue = transaction.revenue,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<GeneralInsuranceBody[]> BranchReport(Customer[] customers, BranchProduct report) {
         GeneralInsuranceBody[] transactions = await new Filter().Transactions<GeneralInsuranceBody, BranchProduct>(report, "general_insurance");
         List<GeneralInsuranceZ> reports = new List<GeneralInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               GeneralInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (tMobile == mobile && customer.location == report.location && customer.branch == report.branch) {
                  reports.Add(new GeneralInsuranceZ() {
                     aadhaar = (long)customer.aadhaar,
                     insurance_type = transaction.insurance_type,
                     manager = transaction.manager,
                     payment_term = transaction.payment_term,
                     product = transaction.product,
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
                     revenue = transaction.revenue,
                  });
               }
            }
         }

         return reports.ToArray();
      }

      public async static Task<GeneralInsuranceBody[]> RMReport(Customer[] customers, Rmproduct report) {
         GeneralInsuranceBody[] transactions = await new Filter().Transactions<GeneralInsuranceBody, Rmproduct>(report, "general_insurance");
         List<GeneralInsuranceZ> reports = new List<GeneralInsuranceZ>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            Customer customer = customers[cIndex];
            long? mobile = customer.mobile;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               GeneralInsuranceBody transaction = transactions[tIndex];
               long tMobile = transaction.mobile;

               if (report.manager == transaction.manager) {
                  reports.Add(new GeneralInsuranceZ() {
                     aadhaar = (long)customer.aadhaar,
                     insurance_type = transaction.insurance_type,
                     manager = transaction.manager,
                     payment_term = transaction.payment_term,
                     product = transaction.product,
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
                     revenue = transaction.revenue,
                  });
               }
            }
         }

         return reports.ToArray();
      }
   }
}
