using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CRM {
   public class IReport : IMongoObject {
      public string LOCATION { get; set; }
      public DateTime END_DATE { get; set; }
      public DateTime START_DATE { get; set; }
   }

   public class IReportObject : ILifeTransaction {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public class Report<TransactionType> : JSON {
      Task<IReport> context;
      string TransactionName;
      public Report(HttpContext Context, string transactionName) {
         context = this.REPORT(Context);
         TransactionName = transactionName;
      }

      private async Task<IReport> REPORT(HttpContext context) {
         return await Deserilise<IReport>(context);
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         IReport report = await this.context;
         string customersStringify = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customersStringify);
      }

      private async Task<ICustomer[]> filterCustomers() {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         IReport report = await this.context;
         List<ICustomer> customers = new List<ICustomer>();

         for (int i = 0; i < listOfCustomers.Length; i++) {
            if (listOfCustomers[i].LOCATION.ToString() == report.LOCATION.ToString()) {
               customers.Add(listOfCustomers[i]);
            }
         }

         return customers.ToArray();
      }

      private async Task<ILifeTransaction[]> filterTransaction() {
         IReport report = await this.context;
         var gte_start = Builders<ILifeTransaction>.Filter
            .Gte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.START_DATE.ToString()));

         var lte_end = Builders<ILifeTransaction>.Filter
            .Lte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.END_DATE.ToString()));

         List<ILifeTransaction> list = Mongo.database.GetCollection<ILifeTransaction>("life_insurance").Find<ILifeTransaction>(gte_start & lte_end).ToList();

         return list.ToArray();
      }

      private async Task<IReportObject[]> fetchReports() {
         ILifeTransaction[] transactions = await this.filterTransaction();
         ICustomer[] customers = await this.filterCustomers();
         List<IReportObject> reports = new List<IReportObject>();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            ICustomer customer = customers[cIndex];
            long? MOBILE = customer.MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               ILifeTransaction transaction = transactions[tIndex];
               long tMobile = transaction.MOBILE;

               if (tMobile == MOBILE) {
                  reports.Add(new IReportObject() {
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

      public async Task<string> customers() {
         return Serialize<IReportObject[]>(await this.fetchReports());
         // return Serialize<ICustomer[]>(await this.filterCustomers());
      }
   }
}