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

   public class Report<TransactionType> : JSON {
      Task<Zonal> context;
      string TransactionName;
      public Report(HttpContext Context, string transactionName) {
         context = this.REPORT(Context);
         TransactionName = transactionName;
      }

      public delegate TransactionType[] Delegate(ICustomer[] customers, Zonal report);

      private async Task<Zonal> REPORT(HttpContext context) {
         return await Deserilise<Zonal>(context);
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         string customersStringify = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customersStringify);
      }

      private async Task<ICustomer[]> filterCustomers() {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         Zonal report = await this.context;
         List<ICustomer> customers = new List<ICustomer>();

         for (int i = 0; i < listOfCustomers.Length; i++) {
            if (listOfCustomers[i].LOCATION.ToString() == report.LOCATION.ToString()) {
               customers.Add(listOfCustomers[i]);
            }
         }

         return customers.ToArray();
      }

      public async Task<string> fetch(Delegate function) {
         return Serialize<TransactionType[]>(
            function(
               await this.fetchAllCustomers(),
               await this.context
            )
         );
      }
   }
}