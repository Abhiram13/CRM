using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class IBranch : IMongoObject {
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

      public async Task<string> FetchAll() {
         return await new Database<IBranch>("branch").FetchAll();
      }

      private async Task<IBranch[]> Branches() {
         string str = await this.FetchAll();
         IBranch[] listOfBranches = DeserializeObject<IBranch[]>(str);
         return listOfBranches;
      }

      private async Task<bool> Check() {
         IBranch[] listOfBranches = await this.Branches();
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
            new Database<IBranch>("branch").Insert(await this.branch);
            return "Branch Successfully Added";
         }

         return "Branch already Added";
      }

   }
}