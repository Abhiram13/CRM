using System;
using System.Collections.Generic;
using MongoDB.Driver;

namespace CRM {
   public class IFixedDeposit : IMongoObject {
      public string COMPANY { get; set; }
      public string PRODUCT { get; set; }
      public string SCHEMA { get; set; }
      public int TENOUR { get; set; }
      public long MOBILE { get; set; }
      public long AADHAAR { get; set; }
      public string ACCOUNT { get; set; }
      public string BANK { get; set; }
      public long AMOUNT { get; set; }
      public long REVENUE { get; set; }
      public DateTime ENTRY_DATE { get; set; }
      public DateTime ISSUANCE_DATE { get; set; }
      public int MANAGER { get; set; }
   }

   public class FixedZonalReport : IFixedDeposit {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public static class FixedDeposit {
      private static IFixedDeposit[] filterTransaction(Zonal report) {
         var gte_start = Builders<IFixedDeposit>.Filter
            .Gte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.START_DATE.ToString()));

         var lte_end = Builders<IFixedDeposit>.Filter
            .Lte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.END_DATE.ToString()));

         List<IFixedDeposit> list = Mongo.database.GetCollection<IFixedDeposit>("general_insurance").Find<IFixedDeposit>(gte_start & lte_end).ToList();

         return list.ToArray();
      }

      public static IFixedDeposit[] Report(ICustomer[] customers, Zonal report) {
         IFixedDeposit[] transactions = filterTransaction(report);
         List<FixedZonalReport> reports = new List<FixedZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IFixedDeposit transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE) {
                  reports.Add(new FixedZonalReport() {
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
