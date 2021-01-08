using System;
using MongoDB.Driver;
using System.Collections.Generic;

namespace CRM {
   public class IMutualFunds : IMongoObject {
      public string AMC { get; set; }
      public string PRODUCT { get; set; }
      public string FUND { get; set; }
      public string PLAN { get; set; }      
      public string OPTION { get; set; }
      public string SUB_OPTION { get; set; }
      public string MODE { get; set; }
      public string ACCOUNT { get; set; }
      public long AMOUNT { get; set; }
      public string BANK { get; set; }
      public int PAYMENT_TERM { get; set; }
      public DateTime ENTRY_DATE { get; set; }
      public DateTime ISSUANCE_DATE { get; set; }
      public long REVENUE { get; set; }
      public long MOBILE { get; set; }
      public long AADHAAR { get; set; }
      public int MANAGER { get; set; }
   }

   public class MutualZonalReport : IMutualFunds {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public static class MutualFunds {
      private static IMutualFunds[] filterTransaction(Zonal report) {
         var gte_start = Builders<IMutualFunds>.Filter
            .Gte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.START_DATE.ToString()));

         var lte_end = Builders<IMutualFunds>.Filter
            .Lte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.END_DATE.ToString()));

         List<IMutualFunds> list = Mongo.database.GetCollection<IMutualFunds>("mutual_funds").Find<IMutualFunds>(gte_start & lte_end).ToList();

         return list.ToArray();
      }

      public static IMutualFunds[] Report(ICustomer[] customers, Zonal report) {
         IMutualFunds[] transactions = filterTransaction(report);
         List<MutualZonalReport> reports = new List<MutualZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               IMutualFunds transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE) {
                  reports.Add(new MutualZonalReport() {
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
