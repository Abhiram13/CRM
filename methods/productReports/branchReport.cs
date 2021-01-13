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