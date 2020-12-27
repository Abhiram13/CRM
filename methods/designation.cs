using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class IDesignation : IMongoObject {
      public string ID { get; set; } = "";
      public string DESIGNATION { get; set; }
   }

   public class Designation : String {
      private HttpContext Context;
      private Task<IDesignation> designation;
      public Designation(HttpContext context) {
         Context = context;
         designation = Deserilise<IDesignation>(context);
      }

      private async Task<IDesignation> editedDesignation() {
         IDesignation desg = await this.designation;
         desg.ID = Encode($"{desg.DESIGNATION}");
         return desg;
      }

      public async Task<string> FetchAll() {
         return await new Database<IDesignation>("designation").FetchAll();
      }

      private async Task<IDesignation[]> Designations() {
         string str = await this.FetchAll();
         IDesignation[] listOfDesignations = DeserializeObject<IDesignation[]>(str);
         return listOfDesignations;
      }

      private async Task<bool> Check() {
         IDesignation[] listOfDesignations = await this.Designations();
         IDesignation currentBranch = await this.designation;

         for (int i = 0; i < listOfDesignations.Length; i++) {
            bool checkDesignation = listOfDesignations[i].DESIGNATION.ToString() == currentBranch.DESIGNATION.ToString();
            if (checkDesignation) return true;
         }

         return false;
      }

      public async Task<string> Add() {
         if (!await this.Check()) {
            new Database<IDesignation>("designation").Insert(await this.editedDesignation());
            return "Designation Successfully Added";
         }

         return "Designation already Added";
      }
   }
}