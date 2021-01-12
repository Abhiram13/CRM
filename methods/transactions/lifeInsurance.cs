using System;
using System.Collections.Generic;
using MongoDB.Driver;
using System.Text.Json;
using System.Threading.Tasks;

namespace CRM {
   public class ILifeTransaction : IMongoObject {
      public long MOBILE { get; set; }
      public long AADHAAR { get; set; }
      public string ACCOUNT { get; set; }
      public string BANK { get; set; }
      public string PLAN { get; set; }
      public int TERM { get; set; }
      public string COMPANY { get; set; }
      public int PREMIUM_PAYMENT_TERM { get; set; }
      public long GROSS { get; set; }
      public long NET { get; set; }
      public long REVENUE { get; set; }
      public DateTime ENTRY_DATE { get; set; }
      public int MANAGER { get; set; }
   }

   public class ZonalReport : ILifeTransaction {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public class Filter : JSON {
      public async Task<TransactionType[]> zonalTransactions<TransactionType>(Zonal report, string transactionName) {
         TransactionType[] transactions = DeserializeObject<TransactionType[]>(await new Database<TransactionType>(transactionName).FetchAll());
         List<TransactionType> ty = new List<TransactionType>();

         for (int i = 0; i < transactions.Length; i++) {
            DateTime entryDate = (DateTime)typeof(TransactionType).GetProperty("ENTRY_DATE").GetValue(transactions[i]);
            DateTime startDate = DateTime.Parse(report.START_DATE.ToString());
            DateTime endDate = DateTime.Parse(report.END_DATE.ToString());

            if (entryDate >= startDate || entryDate <= endDate) {
               ty.Add(transactions[i]);
            }
         }

         return ty.ToArray();
      }
   }

   public static class LifeInsurance {
      public async static Task<ILifeTransaction[]> Report(ICustomer[] customers, Zonal report) {
         ILifeTransaction[] transactions = await new Filter().zonalTransactions<ILifeTransaction>(report, "life_insurance");
         List<ZonalReport> reports = new List<ZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               ILifeTransaction transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE && customer.LOCATION == report.LOCATION) {
                  reports.Add(new ZonalReport() {
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
   }
}
