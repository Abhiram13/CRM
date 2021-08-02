using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   // public class Branch : String {
      // private HttpContext Context;
      // private Task<BranchModel> branch;
      
      // public Branch(HttpContext context) {
      //    Context = context;
      //    branch = Deserilise<BranchModel>(context);
      // }

      // public async Task<string> FetchAll() {
      //    return await new Database<BranchModel>("branch").FetchAll();
      // }

      // private async Task<BranchModel[]> Branches() {
      //    string str = await this.FetchAll();
      //    BranchModel[] listOfBranches = DeserializeObject<BranchModel[]>(str);
      //    return listOfBranches;
      // }

      // private async Task<bool> Check() {
      //    BranchModel[] listOfBranches = await this.Branches();
      //    BranchModel currentBranch = await this.branch;

      //    for (int i = 0; i < listOfBranches.Length; i++) {
      //       bool checkLocation = listOfBranches[i].location.ToString() == currentBranch.location.ToString();
      //       bool checkBranch = listOfBranches[i].branch.ToString() == currentBranch.branch.ToString();
      //       if (checkBranch && checkLocation) return true;
      //    }

      //    return false;
      // }

      // public async Task<string> Add() {
      //    if (!await this.Check()) {
      //       new Database<BranchModel>("branch").Insert(await this.branch);
      //       return "Branch Successfully Added";
      //    }

      //    return "Branch already Added";
      // }

   // }
}