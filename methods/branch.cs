using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class IBranch : IMongoObject {
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
   }

   public class BranchandDesignation<Type> : JSON {
      private HttpContext Context;
      private Task<Type> givenContext;
      private string name;

      public BranchandDesignation(HttpContext context, string Name) {
         Context = context;
         name = Name;
         givenContext = Deserilise<Type>(context);
      }

      private async Task<Type[]> ListOfAll() {
         string list = await new Database<Type>(this.name).FetchAll();
         return DeserializeObject<Type[]>(list);
      }

      public async Task<string> Fetch() {
         Type[] list = await this.ListOfAll();
         List<string> designationsList = new List<string>();

         foreach (Type dsg in list) {
            designationsList.Add(typeof(d));
         }

         return Serialize<string[]>(designationsList.ToArray());
      }

      private async Task<bool> Check() {
         IDesignation[] listOfDesignations = await this.AllDesignations();
         IDesignation dsg = await this.givenDesignation;

         for (int i = 0; i < listOfDesignations.Length; i++) {
            bool isDesignationExist = listOfDesignations[i].DESIGNATION.ToString() == dsg.DESIGNATION.ToString();
            if (isDesignationExist) return true;
         }

         return false;
      }

      public async Task<string> Add() {
         if (!await this.Check()) {
            new Database<IDesignation>("designation").Insert(await this.givenDesignation);
            return "Designation Successfully Added";
         }

         return "Designation already Added";
      }
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