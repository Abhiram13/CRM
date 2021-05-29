using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public class CustomerSearchQuery {
      public long QUERY { get; set; }
   }

   public sealed class Customer : JSON {
      private Task<CustomerModel> customer;

      public Customer(HttpContext context) {
         customer = body(context);
      }

      private async Task<CustomerModel> body(HttpContext context) {
         return await Deserilise<CustomerModel>(context);
      }

      private async Task<string> check() {
         CustomerModel emp = await this.customer;

         foreach (var prop in emp.GetType().GetProperties()) {
            if (prop.GetValue(emp) == null) {
               return $"Please provide {prop.Name.ToLower()}";
            }
         }

         return "OK";
      }

      public static async Task<CustomerModel[]> fetchAllCustomers() {
         string customer = await new Database<CustomerModel>("customer").FetchAll();
         return JSONObject.DeserializeObject<CustomerModel[]>(customer);
      }

      public static async Task<CustomerModel> isCustomerExist(long? mobile, long? aadhaar) {
         CustomerModel[] listOfCustomers = await Customer.fetchAllCustomers();
         return Array.Find<CustomerModel>(listOfCustomers, c => c.MOBILE == mobile && c.AADHAAR == aadhaar);
      }

      public async Task<string> Add() {
         string check = await this.check();
         CustomerModel customer = await this.customer;

         if (check == "OK") {
            if (await Customer.isCustomerExist(customer.MOBILE, customer.AADHAAR) == null) {
               new Database<CustomerModel>("customer").Insert(customer);
               return "Customer Successfully Added";
            }
            return "Customer already Existed";
         }

         return check;
      }

      public async Task<string> search(HttpContext context) {
         CustomerModel[] listOfCustomers = await Customer.fetchAllCustomers();
         CustomerSearchQuery query = await Deserilise<CustomerSearchQuery>(context);
         CustomerModel customer = Array.Find<CustomerModel>(listOfCustomers, customer => customer.MOBILE == query.QUERY || customer.AADHAAR == query.QUERY);

         if (customer == null) {
            return "Customer Not Found";
         }

         return Serialize<CustomerModel>(customer);
      }
   }
}