using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class IBranch : IMongoObject {
      public string ID { get; set; } = "";
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public class Branch : String {
      HttpContext Context;
      Task<IBranch> branch;
      public Branch(HttpContext context) {
         Context = context;
         branch = Deserilise<IBranch>(context);
      }

      private async Task<IBranch> Edit() {
         IBranch brch = await this.branch;
         brch.ID = Encode($"{brch.BRANCH}_{brch.LOCATION}");
         return brch;
      }

      public async Task<string> Add() {
         new Database<IBranch>("branch").Insert(await this.Edit());
         return "Branch Successfully Added";
      }
   }
}