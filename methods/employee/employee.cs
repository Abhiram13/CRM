using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class IEmployee {
      [BsonIgnoreIfNull]
      public MongoDB.Bson.ObjectId _id { get; set; } = new ObjectId();
      public int ID { get; set; } = 0;
      public string TITLE { get; set; } = "";
      public string FIRSTNAME { get; set; } = "";
      public string LASTNAME { get; set; } = "";
      public string EMAIL { get; set; } = "";
      public string PASSWORD { get; set; } = "";
      public string MOBILE { get; set; } = "";
      public string GENDER { get; set; } = "";
      public string DESIGNATION { get; set; } = "";
      public string LOCATION { get; set; } = "";
      public string BRANCH { get; set; } = "";
      public string STATE { get; set; } = "";
      public string REGION { get; set; } = "";
      public string ROLE { get; set; } = "";
      public int? __v { get; } = 1;
   }

   public class Employee : JSON {
      private HttpContext context;
      private Task<IEmployee> employee;
      public Employee(HttpContext Context) {
         context = Context;
         employee = Deserilise<IEmployee>(Context);
      }

      private async Task<string> Check() {
         IEmployee emp = await this.employee;

         foreach (var key in emp.GetType().GetProperties()) {
            bool stringTypeCheck = key.GetValue(emp) is string;
            bool stringValueCheck = key.GetValue(emp).ToString() == "";
            bool intTypeCheck = key.GetValue(emp) is Int32 || key.GetValue(emp) is Int64;
            bool String = stringTypeCheck && stringValueCheck;
            if (String || (intTypeCheck && Convert.ToInt32(key.GetValue(emp)) == 0)) {
               return $"{key} cannot be Empty";
            }
         }

         return "OK";
      }

      private async Task<bool> isEmployeeExist(IEmployee employee) {
         string employees = await new Database<IEmployee>("employee").FetchAll();
         IEmployee[] listOfEmployees = DeserializeObject<IEmployee[]>(employees);

         foreach (IEmployee emp in listOfEmployees) {
            if (employee.MOBILE == emp.MOBILE && employee.ID == emp.ID) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         string check = await this.Check();
         IEmployee employee = await this.employee;

         // checks if employee request body object is OKAY
         if (check == "OK") {

            // boolean value tells whether if given already existed
            // in database and return boolean
            bool isExist = await this.isEmployeeExist(employee);

            // if user does not exist, then add employee to the database
            if (!isExist) {
               new Database<IEmployee>("employee").Insert(employee);
               return "Employee Successfully Added";
            }

            // else return following response
            return "Employee already Existed";
         }

         return check;
      }
   }
}