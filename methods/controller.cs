using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CRM {
   // public sealed partial class EmployeeController {
   //    private Task<Employee> employee;

   //    public EmployeeController(HttpContext Context) {
   //       this.employee = JSONObject.Deserilise<Employee>(Context);
   //    }

   //    public async Task<string> Add() {
   //       bool isEmployeeExist = await EmployeeController.IsEmployeeExist(this.employee.Id);

   //       if (!isEmployeeExist) {
   //          new Database<Employee>(Table.employee).Insert(await this.employee);
   //          return "Employee Successfully Added";
   //       }

   //       return "Employee already Existed";
   //    }
   // }
   public abstract class Controller {
      private HttpContext context;

      public Controller(HttpContext Context) {
         this.context = Context;
      }

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

      public string Add<Table>(bool b, string table, Table doc) {
         if (!b) {
            new Database<Table>(table).Insert(doc);
            return "Successfully Added";
         }

         return "Already Existed";
      }
   }
}