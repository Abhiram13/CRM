using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;

namespace CRM {
   public class IReport : IMongoObject {
      public string LOCATION { get; set; }
      public DateTime END_DATE { get; set; }
      public DateTime START_DATE { get; set; }
   }

   public class Report : JSON {
      Task<IReport> context;
      public Report(HttpContext Context) {
         context = this.REPORT(Context);
      }

      private async Task<IReport> REPORT(HttpContext context) {
         return await Deserilise<IReport>(context);
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         IReport report = await this.context;
         string customersStringify = await new Database<ICustomer>("customer").FetchAll();
         Console.WriteLine(customersStringify);
         return DeserializeObject<ICustomer[]>(customersStringify);
      }

      private async Task<ICustomer[]> filterNumbers() {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         IReport report = await this.context;
         List<ICustomer> customers = new List<ICustomer>();

         for (int i = 0; i < listOfCustomers.Length; i++) {
            if (listOfCustomers[i].LOCATION.ToString() == report.LOCATION.ToString()) {
               customers.Add(listOfCustomers[i]);
            }
         }

         return customers.ToArray();
      }

      public async Task<string> customers() {
         return Serialize<ICustomer[]>(await this.filterNumbers());
      }
   }
}