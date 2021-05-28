using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {   
   public sealed class Transactions {
      /// <summary>
      /// Fetches Transactions within Date Range
      /// </summary>
      /// <typeparam name="T">Type of Transaction</typeparam>
      /// <typeparam name="P">Type of Product</typeparam>
      /// <param name="productReport"></param>
      /// <param name="transactionName"></param>
      /// <returns>List of Transactions that filtered within date range of given product</returns>
      public async static Task<List<T>> FetchFromDateRange<T, P>(P productReport, string transactionName) {
         string allTransactions = await new Database<T>(transactionName).FetchAll();
         T[] transactions = JSONObject.DeserializeObject<T[]>(allTransactions);
         List<T> transactionsList = new List<T>();

         for (int i = 0; i < transactions.Length; i++) {
            DateTime entryDate = (DateTime)typeof(T).GetProperty("ENTRY_DATE").GetValue(transactions[i]);
            DateTime startDate = DateTime.Parse(typeof(P).GetProperty("START_DATE").GetValue(productReport).ToString());
            DateTime endDate = DateTime.Parse(typeof(P).GetProperty("END_DATE").GetValue(productReport).ToString());

            if (entryDate >= startDate || entryDate <= endDate) {
               transactionsList.Add(transactions[i]);
            }
         }

         return transactionsList;
      }
   }

   public class Filter : JSON {
      public async Task<TransactionType[]> Transactions<TransactionType, ProductType>(ProductType report, string transactionName) {
         TransactionType[] transactions = DeserializeObject<TransactionType[]>(await new Database<TransactionType>(transactionName).FetchAll());
         List<TransactionType> ty = new List<TransactionType>();

         for (int i = 0; i < transactions.Length; i++) {
            DateTime entryDate = (DateTime)typeof(TransactionType).GetProperty("ENTRY_DATE").GetValue(transactions[i]);
            DateTime startDate = DateTime.Parse(typeof(ProductType).GetProperty("START_DATE").GetValue(report).ToString());
            DateTime endDate = DateTime.Parse(typeof(ProductType).GetProperty("END_DATE").GetValue(report).ToString());

            if (entryDate >= startDate || entryDate <= endDate) {
               ty.Add(transactions[i]);
            }
         }

         return ty.ToArray();
      }
   }
}