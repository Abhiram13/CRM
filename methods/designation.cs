using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CRM {
   public class IDesignation : IMongoObject {
      public string DESIGNATION { get; set; }
   }

   public class Designation : String {
      private HttpContext Context;
      private Task<IDesignation> givenDesignation;
      public Designation(HttpContext context) {
         Context = context;
         givenDesignation = Deserilise<IDesignation>(context);
      }

      private async Task<IDesignation[]> AllDesignations() {
         string designations = await new Database<IDesignation>("designation").FetchAll();
         return DeserializeObject<IDesignation[]>(designations);
      }

      public async Task<string> FetchDesignations() {
         IDesignation[] listOfDesignations = await this.AllDesignations();
         List<string> designationsList = new List<string>();

         foreach (IDesignation dsg in listOfDesignations) {
            designationsList.Add(dsg.DESIGNATION);
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
}