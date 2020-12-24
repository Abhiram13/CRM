using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class Transaction<TransactionType> : JSON {
      HttpContext Context;
      Task<TransactionType> transaction;
      string name;
      public Transaction(HttpContext context, string transactionName) {
         Context = context;
         transaction = Deserilise<TransactionType>(context);
         name = transactionName;
      }

      public async Task<string> Add() {
         new Database<TransactionType>(this.name).Insert(await this.transaction);
         return "Transaction successfully added";
      }
   }
}