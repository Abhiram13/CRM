using System;
using System.Threading.Tasks;

namespace CRM {
   public abstract class Controller {
      /// <summary>
      /// Fetches all documents from the specified table
      /// </summary>
      /// <typeparam name="Table">Table Type</typeparam>
      /// <param name="tableName">Name of the Table from where documents need to be fetched</param>
      /// <returns>Array of Documents from the specified table</returns>
      public async static Task<Table[]> FetchAll<Table>(string tableName) {
         string documents = await new Database<Table>(tableName).FetchAll();
         return JSONObject.DeserializeObject<Table[]>(documents);
      }
   }
}