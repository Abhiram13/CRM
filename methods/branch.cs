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

      private async Task<bool> Check() {
         string str = await new Database<IBranch>("branch").FetchAll();
         IBranch[] listOfBranches = DeserializeObject<IBranch[]>(str);
         IBranch currentBranch = await this.branch;

         for (int i = 0; i < listOfBranches.Length; i++) {
            bool checkLocation = listOfBranches[i].LOCATION.ToString() == currentBranch.LOCATION.ToString();
            bool checkBranch = listOfBranches[i].BRANCH.ToString() == currentBranch.BRANCH.ToString();
            if (checkBranch && checkLocation) return true;
         }

         return false;
      }

      public async Task<string> Add() {
         if (!await this.Check()) {
            new Database<IBranch>("branch").Insert(await this.Edit());
            return "Branch Successfully Added";
         }

         return "Branch already Added";
      }
   }
}