using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class Transaction<TransactionType> : JSON {
      private HttpContext Context;
      private Task<TransactionType> transaction;
      private string name;
      public Transaction(HttpContext context, string transactionName) {
         Context = context;
         transaction = Deserilise<TransactionType>(context);
         name = transactionName;
      }

      private async Task<ICustomer> checkIfCustomerExist() {
         ICustomer[] customers = DeserializeObject<ICustomer[]>(await new Database<ICustomer[]>("customer").FetchAll());
         var MOBILE = typeof(TransactionType).GetProperty("MOBILE").GetValue(await this.transaction);
         var AADHAAR = typeof(TransactionType).GetProperty("AADHAAR").GetValue(await this.transaction);

         ICustomer c = System.Array.Find<ICustomer>(customers, cust => cust.AADHAAR == (long)AADHAAR && cust.MOBILE == (long)MOBILE);
         return c;
      }

      public async Task<string> Add() {
         if (await this.checkIfCustomerExist() == null) {
            return "Customer not Found";
         }

         new Database<TransactionType>(this.name).Insert(await this.transaction);
         return "Transaction successfully added";
      }
   }
}