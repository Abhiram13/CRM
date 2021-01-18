using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class Zonal : IMongoObject {
      public string LOCATION { get; set; }
      public DateTime END_DATE { get; set; }
      public DateTime START_DATE { get; set; }
   }

   public class ZonalReport : ILifeTransaction {
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
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