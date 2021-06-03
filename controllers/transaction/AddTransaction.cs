using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;
using CRM;

namespace TransactionManagement {
   public partial class TransactionController {
      // private HttpContext context;      

      // public TransactionController(HttpContext Context) {
      //    this.context = Context;         
      // }

      // public async Task AddTransaction<T>(bool employee, bool customer, string table, T document) {
      //    if (employee && customer) {
      //       new Database<T>(table).Insert(document);
      //    }
      // }

      public void AddTransaction<T>(TransactionVerification<T> transaction) {
         if (transaction.isEmployeeExist && transaction.isCustomerExist) {
            new Database<T>(transaction.table).Insert(transaction.document);
         }
      }
   }
}