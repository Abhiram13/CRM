using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using MongoDB.Driver;

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
   }

   public class ZonalReport : ILifeTransaction {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public static class LifeInsurance {
      private static ILifeTransaction[] filterTransaction(Zonal report) {
         var gte_start = Builders<ILifeTransaction>.Filter
            .Gte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.START_DATE.ToString()));

         var lte_end = Builders<ILifeTransaction>.Filter
            .Lte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.END_DATE.ToString()));

         List<ILifeTransaction> list = Mongo.database.GetCollection<ILifeTransaction>("life_insurance").Find<ILifeTransaction>(gte_start & lte_end).ToList();

         return list.ToArray();
      }

      public static ILifeTransaction[] Report(ICustomer[] customers, Zonal report) {
         ILifeTransaction[] transactions = filterTransaction(report);
         List<ZonalReport> reports = new List<ZonalReport>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               ILifeTransaction transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE) {
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
