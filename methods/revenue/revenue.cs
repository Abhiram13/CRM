using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class Revenue<RevenueType> : JSON {
      HttpContext Context;
      Task<RevenueType> revenue;
      string name;
      public Revenue(HttpContext context, string revenueName) {
         Context = context;
         revenue = Deserilise<RevenueType>(context);
         name = revenueName;
      }

      public async Task<string> Add() {
         new Database<RevenueType>(this.name).Insert(await this.revenue);
         return "Revenue successfully added";
      }
   }
}