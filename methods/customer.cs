using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
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

         foreach (var prop in emp.GetType().GetProperties()) {
            if (prop.GetValue(emp) == null) {
               return $"Please provide {prop.Name.ToLower()}";
            }            
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