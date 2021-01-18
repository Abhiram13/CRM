using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class IBranchBody : Zonal {
      public string BRANCH { get; set; }
   }

   public class FetchReports<TransactionType, ReportType> : JSON {
      Task<ReportType> context;
      string TransactionName;

      public FetchReports(HttpContext Context, string transactionName) {
         context = this.REPORT(Context);
         TransactionName = transactionName;
      }

      public delegate Task<TransactionType[]> Delegate(ICustomer[] customers, ReportType report);

      private async Task<ReportType> REPORT(HttpContext context) {
         return await Deserilise<ReportType>(context);
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         string customersStringify = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customersStringify);
      }

      public async Task<string> fetch(Delegate function) {
         return Serialize<TransactionType[]>(
            await function(
               await this.fetchAllCustomers(),
               await this.context
            )
         );
      }
   }
}