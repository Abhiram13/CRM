using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   // public class FetchReports<TransactionType, ReportType> : JSON {
   //    Task<ReportType> context;
   //    string TransactionName;

   //    public FetchReports(HttpContext Context, string transactionName) {
   //       context = this.REPORT(Context);
   //       TransactionName = transactionName;
   //    }

   //    public delegate Task<TransactionType[]> Delegate(Customer[] customers, ReportType report);

   //    private async Task<ReportType> REPORT(HttpContext context) {
   //       return await Deserilise<ReportType>(context);
   //    }

   //    private async Task<Customer[]> fetchAllCustomers() {
   //       string customersStringify = await new Database<Customer>("customer").FetchAll();
   //       return DeserializeObject<Customer[]>(customersStringify);
   //    }

   //    public async Task<string> fetch(Delegate function) {
   //       return Serialize<TransactionType[]>(
   //          await function(
   //             await this.fetchAllCustomers(),
   //             await this.context
   //          )
   //       );
   //    }
   // }
}
