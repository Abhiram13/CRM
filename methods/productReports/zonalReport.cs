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
}