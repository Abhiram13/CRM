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
         // var gte_start = Builders<TransactionType>.Filter
         //    .Gte(transaction => (DateTime) typeof(TransactionType).GetProperty("ENTRY_DATE").GetValue(transaction, null), DateTime.Parse(report.START_DATE.ToString()));

         // var lte_end = Builders<TransactionType>.Filter
         //    .Lte(transaction => (DateTime) typeof(TransactionType).GetProperty("ENTRY_DATE").GetValue(transaction, null), DateTime.Parse(report.END_DATE.ToString()));

         // List<TransactionType> list = Mongo.database.GetCollection<TransactionType>(this.TransactionName).Find<TransactionType>(gte_start & lte_end).ToList();

         // foreach (TransactionType t in list) {
         //    Console.WriteLine(t.GetType().GetProperty("ENTRY_DATE").GetValue(t));
         // }

         // return list.ToArray();

         var gte_start = Builders<ILifeTransaction>.Filter
            .Gte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.START_DATE.ToString()));

         var lte_end = Builders<ILifeTransaction>.Filter
         .Lte(transaction => transaction.ENTRY_DATE, DateTime.Parse(report.END_DATE.ToString()));

         List<ILifeTransaction> list = Mongo.database.GetCollection<ILifeTransaction>("life_insurance").Find<ILifeTransaction>(gte_start & lte_end).ToList();

         return list.ToArray();
      }

      private async void fetchReports() {
         ILifeTransaction[] transactions = await this.filterTransaction();
         ICustomer[] customers = await this.filterCustomers();

         for (int cIndex = 0; cIndex < customers.Length; cIndex++) {
            long MOBILE = customers[cIndex].MOBILE;

            for (int tIndex = 0; tIndex < transactions.Length; tIndex++) {
               long tMobile = (long)transactions[tIndex].GetType().GetProperty("MOBILE").GetValue(transactions[tIndex]);

               if (tMobile == MOBILE) {
                  Console.WriteLine(transactions[tIndex]);
               }
            }
         }
      }

      public async Task<string> customers() {
         this.fetchReports();
         return Serialize<ICustomer[]>(await this.filterCustomers());
      }
   }
}