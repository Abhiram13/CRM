using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public sealed partial class Transactions {
      private HttpContext Context;

      public Transactions(HttpContext context) {
         this.Context = context;
      }

      private async Task<CustomerDetails> GivenTransaction<T>() {
         T trans = await JSONObject.Deserilise<T>(this.Context);
         var mobile = typeof(T).GetProperty("MOBILE").GetValue(trans);
         var aadhaar = typeof(T).GetProperty("AADHAAR").GetValue(trans);

         return new CustomerDetails() {
            aadhaar = (long)aadhaar,
            mobile = (long)mobile
         };
      }

      /// <summary>
      /// Adds given transaction to the database
      /// </summary>
      /// <typeparam name="T">Type of Transaction</typeparam>
      /// <returns></returns>
      public async Task Add<T>() {
         CustomerDetails details = await GivenTransaction<T>();

         if (Customer.isCustomerExist(details.mobile, details.aadhaar) == null) { }
      }
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

      private async Task<CustomerModel> checkIfCustomerExist() {         
         CustomerModel[] customers = DeserializeObject<CustomerModel[]>(await new Database<CustomerModel>("customer").FetchAll());
         var MOBILE = typeof(TransactionType).GetProperty("MOBILE").GetValue(await this.transaction);
         var AADHAAR = typeof(TransactionType).GetProperty("AADHAAR").GetValue(await this.transaction);

         CustomerModel c = System.Array.Find<CustomerModel>(customers, cust => cust.AADHAAR == (long)AADHAAR && cust.MOBILE == (long)MOBILE);
         return c;
      }

      private async Task<EmployeeModel> checkIfEmployeeExist() {
         EmployeeModel[] employees = DeserializeObject<EmployeeModel[]>(await new Database<EmployeeModel>("employee").FetchAll());
         var MANAGER = typeof(TransactionType).GetProperty("MANAGER").GetValue(await this.transaction);

         EmployeeModel emp = System.Array.Find<EmployeeModel>(employees, emp => emp.ID == (int)MANAGER);

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