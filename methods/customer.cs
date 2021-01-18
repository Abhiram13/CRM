using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CRM {
   public class ICustomer : IMongoObject {
      public string TITLE { get; set; }
      public string FIRSTNAME { get; set; }
      public string LASTNAME { get; set; }
      public long? MOBILE { get; set; }
      public string EMAIL { get; set; }
      public DateTime? BIRTHDATE { get; set; }
      public string GENDER { get; set; }
      public string PAN { get; set; }
      public long? AADHAAR { get; set; }
      public string LOCATION { get; set; }
      public string BRANCH { get; set; }
      public string PRESENT_LINE_1 { get; set; }
      public string PRESENT_LINE_2 { get; set; }
      public string PRESENT_CITY { get; set; }
      public string PRESENT_DISTRICT { get; set; }
      public string PRESENT_STATE { get; set; }
      public string PRESENT_COUNTRY { get; set; }
      public long PRESENT_PINCODE { get; set; }
      public bool IS_PERMANENT { get; set; } = true;
      public string PERMANENT_LINE_1 { get; set; } = null;
      public string PERMANENT_LINE_2 { get; set; } = null;
      public string PERMANENT_CITY { get; set; } = null;
      public string PERMANENT_DISTRICT { get; set; } = null;
      public string PERMANENT_STATE { get; set; } = null;
      public string PERMANENT_COUNTRY { get; set; } = null;
      public long PERMANENT_PINCODE { get; set; } = 0;
   }

   public class CustomerSearchQuery {
      public long QUERY { get; set; }
   }

   public sealed class Customer : JSON {
      private Task<ICustomer> customer;

      public Customer(HttpContext context) {
         customer = body(context);
      }

      private async Task<ICustomer> body(HttpContext context) {
         return await Deserilise<ICustomer>(context);
      }

      private async Task<string> check() {
         ICustomer emp = await this.customer;
         string ext = "Please Provide";

         if (emp.AADHAAR == null) {
            return $"{ext} Aadhaar";
         } else if (emp.BIRTHDATE == null) {
            return $"{ext} Date of Birth";
         } else if (emp.BRANCH == "" || emp.BRANCH == null) {
            return $"{ext} Branch";
         } else if (emp.EMAIL == "" || emp.EMAIL == null) {
            return $"{ext} Email";
         } else if (emp.FIRSTNAME == "" || emp.FIRSTNAME == null) {
            return $"{ext} Firstname";
         } else if (emp.LASTNAME == "" || emp.LASTNAME == null) {
            return $"{ext} Lastname";
         } else if (emp.GENDER == "" || emp.GENDER == null) {
            return $"{ext} Gender";
         } else if (emp.MOBILE == null) {
            return $"{ext} Mobile";
         }

         return "OK";
      }

      private async Task<ICustomer[]> fetchAllCustomers() {
         string customer = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customer);
      }

      private async Task<bool> isExist() {
         ICustomer[] listOfCustomers = await fetchAllCustomers();
         ICustomer customer = await this.customer;

         foreach (ICustomer cust in listOfCustomers) {
            if (customer.MOBILE == cust.MOBILE || customer.AADHAAR == cust.AADHAAR) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         string check = await this.check();
         ICustomer customer = await this.customer;

         if (check == "OK") {
            if (!await isExist()) {
               new Database<ICustomer>("customer").Insert(customer);
               return "Customer Successfully Added";
            }
            return "Customer already Existed";
         }

         return check;
      }

      public async Task<string> search(HttpContext context) {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();
         CustomerSearchQuery query = await Deserilise<CustomerSearchQuery>(context);
         ICustomer customer = Array.Find<ICustomer>(listOfCustomers, customer => customer.MOBILE == query.QUERY || customer.AADHAAR == query.QUERY);

         if (customer == null) {
            return "Customer Not Found";
         }

         return Serialize<ICustomer>(customer);
      }
   }
}