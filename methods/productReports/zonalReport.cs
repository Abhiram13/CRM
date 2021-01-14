using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class Zonal : IMongoObject {
      public string LOCATION { get; set; }
      public DateTime END_DATE { get; set; }
      public DateTime START_DATE { get; set; }
   }   

   public class ZonalReport<TransactionType> : JSON {
      Task<Zonal> context;
      string TransactionName;
      public ZonalReport(HttpContext Context, string transactionName) {
         context = this.REPORT(Context);
         TransactionName = transactionName;
      }

      public delegate Task<TransactionType[]> Delegate(ICustomer[] customers, Zonal report);

      private async Task<Zonal> REPORT(HttpContext context) {
         return await Deserilise<Zonal>(context);
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         string customersStringify = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customersStringify);
      }

      public async Task<string> fetch(Delegate function) {
         return Serialize<TransactionType[]>(
            await function(await this.fetchAllCustomers(), await this.context)
         );
      }
   }

   public class Filter : JSON {
      public async Task<TransactionType[]> Transactions<TransactionType, ProductType>(ProductType report, string transactionName) {
         TransactionType[] transactions = DeserializeObject<TransactionType[]>(await new Database<TransactionType>(transactionName).FetchAll());
         List<TransactionType> ty = new List<TransactionType>();

         for (int i = 0; i < transactions.Length; i++) {
            DateTime entryDate = (DateTime)typeof(TransactionType).GetProperty("ENTRY_DATE").GetValue(transactions[i]);
            DateTime startDate = DateTime.Parse(typeof(ProductType).GetProperty("START_DATE").GetValue(report).ToString());
            DateTime endDate = DateTime.Parse(typeof(ProductType).GetProperty("END_DATE").GetValue(report).ToString());

            if (entryDate >= startDate || entryDate <= endDate) {
               ty.Add(transactions[i]);
            }
         }

         return ty.ToArray();
      }
   }
}