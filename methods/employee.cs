using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public class Employee : JSON {
      private HttpContext context;
      private Task<EmployeeModel> employee;
      
      public Employee(HttpContext Context) {
         context = Context;
         employee = Deserilise<EmployeeModel>(Context);
      }

      private async Task<string> Check() {
         EmployeeModel emp = await this.employee;

         foreach (var key in emp.GetType().GetProperties()) {
            bool stringTypeCheck = key.GetValue(emp) is string;
            bool stringValueCheck = key.GetValue(emp).ToString() == "";
            bool intTypeCheck = key.GetValue(emp) is Int32 || key.GetValue(emp) is Int64 || key.GetValue(emp) is long;
            bool String = stringTypeCheck && stringValueCheck;
            if (String || (intTypeCheck && Convert.ToInt64(key.GetValue(emp)) == 0)) {
               return $"{key} cannot be Empty";
            }
         }

         return "OK";
      }

      public async Task<EmployeeModel[]> fetchAllEmployees() {
         string employee = await new Database<EmployeeModel>("employee").FetchAll();
         return DeserializeObject<EmployeeModel[]>(employee);
      }

      public async Task<string> fetchEmployeeById(string id) {
         EmployeeModel[] employeesList = await this.fetchAllEmployees();
         EmployeeModel Employee = Array.Find<EmployeeModel>(employeesList, employee => employee.ID.ToString() == id);
         return Serialize<EmployeeModel>(Employee);
      }

      private async Task<bool> isEmployeeExist(EmployeeModel employee) {
         EmployeeModel[] listOfEmployees = await this.fetchAllEmployees();

         foreach (EmployeeModel emp in listOfEmployees) {
            if (employee.MOBILE == emp.MOBILE || employee.ID == emp.ID || employee.EMAIL == emp.EMAIL) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         string check = await this.Check();
         EmployeeModel employee = await this.employee;

         // checks if employee request body object is OKAY
         if (check == "OK") {

            // boolean value tells whether if given employee already existed in database
            bool isExist = await this.isEmployeeExist(employee);

            // if user does not exist, then add employee to the database
            if (!isExist) {
               new Database<EmployeeModel>("employee").Insert(employee);
               return "Employee Successfully Added";
            }

            // else return following response
            return "Employee already Existed";
         }

         return check;
      }

      public static string Roles() {
         string[] roles = new string[] { 
            "Admin", 
            "Zonal Manager", 
            "Branch Manager", 
            "Relational Manager", 
            "Sr.Relational Manager", 
            "National Sales Manager", 
            "Regional Sales Manager",
            "Assistant Manager", 
            "Team Leader", 
            "Sr.Team Leader", 
            "Telesales Manager", 
            "Backend Operations", 
            "Account Manager", 
            "HR" 
         };

         return SerializeStatic<string[]>(roles);
      }
   }
}