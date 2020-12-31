using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class ICustomer : IMongoObject {
      public string TITLE { get; set; }
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public long MOBILE { get; set; }
      public string EMAIL { get; set; }
      public DateTime BIRTHDATE { get; set; }
      public string GENDER { get; set; }
      public string PAN { get; set; }
      public long AADHAAR { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
      public string PRESENT_LINE_1 { get; set; } = "";
      public string PRESENT_LINE_2 { get; set; } = "";
      public string PRESENT_CITY { get; set; } = "";
      public string PRESENT_DISTRICT { get; set; } = "";
      public string PRESENT_STATE { get; set; } = "";
      public string PRESENT_COUNTRY { get; set; } = "";
      public long PRESENT_PINCODE { get; set; } = 0;
      public bool IS_PERMANENT { get; set; } = true;
      public string PERMANENT_LINE_1 { get; set; } = "";
      public string PERMANENT_LINE_2 { get; set; } = "";
      public string PERMANENT_CITY { get; set; } = "";
      public string PERMANENT_DISTRICT { get; set; } = "";
      public string PERMANENT_STATE { get; set; } = "";
      public string PERMANENT_COUNTRY { get; set; } = "";
      public long PERMANENT_PINCODE { get; set; } = 0;
   }

   public class CustomerSearchQuery {
      public long QUERY { get; set; }
   }

   public sealed class Customer : JSON {
      private HttpContext Context;

      public Customer(HttpContext context) {
         Context = context;
      }

      private async Task<ICustomer> body() {
         return await Deserilise<ICustomer>(this.Context);
      }

      private async Task<string> check() {
         ICustomer emp = await this.body();

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

      private async Task<ICustomer[]> fetchAllCustomers() {
         string customer = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customer);
      }

      private async Task<bool> isExist() {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         ICustomer customer = await this.body();

         foreach (ICustomer cust in listOfCustomers) {
            if (customer.MOBILE == cust.MOBILE && customer.AADHAAR == cust.AADHAAR) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         string check = await this.check();
         ICustomer customer = await this.body();

         if (check == "OK") {
            if (!await this.isExist()) {
               new Database<ICustomer>("customer").Insert(customer);
               return "Customer Successfully Added";
            }
            return "Customer already Existed";
         }

         return check;
      }

      public async Task<string> search() {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         CustomerSearchQuery query = await Deserilise<CustomerSearchQuery>(this.Context);
         ICustomer customer = Array.Find<ICustomer>(listOfCustomers, customer => customer.MOBILE == query.QUERY || customer.AADHAAR == query.QUERY);

         if (customer == null) {
            return "Customer Not Found";
         }

         return Serialize<ICustomer>(customer);
      }
   }
}