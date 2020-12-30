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

   public class Customer : JSON {
      private HttpContext context;
      private Task<ICustomer> CUSTOMER;

      public Customer(HttpContext Context) {
         context = Context;
         CUSTOMER = Deserilise<ICustomer>(Context);
      }

      private async Task<string> Check() {
         ICustomer emp = await this.CUSTOMER;

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

      public async Task<ICustomer[]> fetchAllCustomers() {
         string customer = await new Database<ICustomer>("customer").FetchAll();
         return DeserializeObject<ICustomer[]>(customer);
      }

      // public async Task<string> fetchEmployeeById(string id) {
      //    ICustomer[] customersList = await this.fetchAllCustomers();
      //    ICustomer Employee = Array.Find<ICustomer>(customersList, customer => customer..ToString() == id);
      //    return Serialize<ICustomer>(Employee);
      // }

      private async Task<bool> isCustomerExist(ICustomer customer) {
         ICustomer[] listOfCustomers = await this.fetchAllCustomers();

         foreach (ICustomer cust in listOfCustomers) {
            if (customer.MOBILE == cust.MOBILE && customer.AADHAAR == cust.AADHAAR) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         string check = await this.Check();
         ICustomer customer = await this.CUSTOMER;

         // checks if employee request body object is OKAY
         if (check == "OK") {

            // boolean value tells whether if given employee already existed in database
            bool isExist = await this.isCustomerExist(customer);

            // if user does not exist, then add employee to the database
            if (!isExist) {
               new Database<ICustomer>("customer").Insert(customer);
               return "Customer Successfully Added";
            }

            // else return following response
            return "Customer already Existed";
         }

         return check;
      }
   }

   public class Query : JSON {
      private HttpContext context;
      private Task<CustomerSearchQuery> Obj;
      public Query(HttpContext Context) {
         context = Context;
         Obj = Deserilise<CustomerSearchQuery>(Context);
      }

      public async Task<string> Search() {
         ICustomer[] listOfCustomers = await new Customer(this.context).fetchAllCustomers();
         CustomerSearchQuery query = await this.Obj;
         ICustomer Customer = Array.Find<ICustomer>(listOfCustomers, customer => customer.MOBILE == query.QUERY || customer.AADHAAR == query.QUERY);

         if (Customer == null) {
            return "Customer Not Found";
         }

         return Serialize<ICustomer>(Customer);
      }
   }
}