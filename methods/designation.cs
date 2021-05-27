using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using Models;

namespace CRM {
   public class Designation : JSON {
      private HttpContext Context;
      private Task<DesignationModel> givenDesignation;
      public Designation(HttpContext context) {
         Context = context;
         givenDesignation = Deserilise<DesignationModel>(context);
      }

      private async Task<DesignationModel[]> AllDesignations() {
         string designations = await new Database<DesignationModel>("designation").FetchAll();
         return DeserializeObject<DesignationModel[]>(designations);
      }

      public async Task<string> FetchDesignations() {
         DesignationModel[] listOfDesignations = await this.AllDesignations();
         List<string> designationsList = new List<string>();

         foreach (DesignationModel dsg in listOfDesignations) {
            designationsList.Add(dsg.DESIGNATION);
         }

         return Serialize<string[]>(designationsList.ToArray());
      }

      private async Task<bool> Check() {
         DesignationModel[] listOfDesignations = await this.AllDesignations();
         DesignationModel dsg = await this.givenDesignation;

         for (int i = 0; i < listOfDesignations.Length; i++) {
            bool isDesignationExist = listOfDesignations[i].DESIGNATION.ToString() == dsg.DESIGNATION.ToString();
            if (isDesignationExist) return true;
         }

         return false;
      }

      public async Task<string> Add() {
         if (!await this.Check()) {
            new Database<DesignationModel>("designation").Insert(await this.givenDesignation);
            return "Designation Successfully Added";
         }

         return "Designation already Added";
      }
   }
}