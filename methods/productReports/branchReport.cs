using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class IBranchBody : Zonal {
      public string BRANCH { get; set; }
   }

   public class BranchReport<TransactionType> : JSON {
      Task<IBranchBody> context;
      string TransactionName;
      public BranchReport(HttpContext Context, string transactionName) {
         context = this.REPORT(Context);
         TransactionName = transactionName;
      }

      public delegate TransactionType[] Delegate(ICustomer[] customers, IBranchBody report);

      private async Task<IBranchBody> REPORT(HttpContext context) {         
         return await Deserilise<IBranchBody>(context);
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         string customersStringify = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customersStringify);
      }

      private async Task<ICustomer[]> filterCustomers() {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         IBranchBody report = await this.context;
         List<ICustomer> customers = new List<ICustomer>();

         for (int i = 0; i < listOfCustomers.Length; i++) {
            if (listOfCustomers[i].LOCATION.ToString() == report.LOCATION.ToString() && listOfCustomers[i].BRANCH.ToString() == report.BRANCH.ToString()) {
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