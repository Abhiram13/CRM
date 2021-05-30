using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public sealed partial class Transactions {
      
   }

   public class Transaction<TransactionType> : JSON {
      private HttpContext Context;
      private Task<TransactionType> transaction;
      private string name;
      public Transaction(HttpContext context, string transactionName) {
         Context = context;
         transaction = Deserilise<TransactionType>(context);
         name = transactionName;
      }

      private async Task<Customer> checkIfCustomerExist() {         
         Customer[] customers = DeserializeObject<Customer[]>(await new Database<Customer>("customer").FetchAll());
         var MOBILE = typeof(TransactionType).GetProperty("MOBILE").GetValue(await this.transaction);
         var AADHAAR = typeof(TransactionType).GetProperty("AADHAAR").GetValue(await this.transaction);

         Customer c = System.Array.Find<Customer>(customers, cust => cust.AADHAAR == (long)AADHAAR && cust.MOBILE == (long)MOBILE);
         return c;
      }

      private async Task<Employee> checkIfEmployeeExist() {
         Employee[] employees = DeserializeObject<Employee[]>(await new Database<Employee>("employee").FetchAll());
         var MANAGER = typeof(TransactionType).GetProperty("MANAGER").GetValue(await this.transaction);

         Employee emp = System.Array.Find<Employee>(employees, emp => emp.ID == (int)MANAGER);

         return emp;
      }

      public async Task<string> Add() {
         if (await this.checkIfCustomerExist() == null) {
            return "Customer not Found";
         }

         if (await this.checkIfEmployeeExist() == null) {
            return "Employee not Found";
         }

         new Database<TransactionType>(this.name).Insert(await this.transaction);
         return "Transaction successfully added";
      }
   }
}