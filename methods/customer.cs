using System;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Models;

namespace CRM {
   public class CustomerSearchQuery {
      public long QUERY { get; set; }
   }

   public sealed class Cust : JSON {
      private Task<CustomerModel> customer;

      public Cust(HttpContext context) {
         customer = body(context);
      }

      private async Task<CustomerModel> body(HttpContext context) {
         return await Deserilise<CustomerModel>(context);
      }

      public async static Task<CustomerModel[]> fetchAllCustomers() {
         string customer = await new Database<CustomerModel>(Table.customer).FetchAll();
         return new JSON().DeserializeObject<CustomerModel[]>(customer);
      }
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

      private async Task<CustomerModel[]> fetchAllCustomers() {
         string customer = await new Database<CustomerModel>("customer").FetchAll();
         return DeserializeObject<CustomerModel[]>(customer);
      }

      private async Task<bool> isExist() {
         CustomerModel[] listOfCustomers = await fetchAllCustomers();
         CustomerModel customer = await this.customer;

         foreach (CustomerModel cust in listOfCustomers) {
            if (customer.MOBILE == cust.MOBILE || customer.AADHAAR == cust.AADHAAR) {
               return true;
            }
         }

         return false;
      }

      public async Task<string> Add() {
         string check = await this.check();
         CustomerModel customer = await this.customer;

         if (check == "OK") {
            if (!await isExist()) {
               new Database<CustomerModel>("customer").Insert(customer);
               return "Customer Successfully Added";
            }
            return "Customer already Existed";
         }

         return check;
      }

      public async Task<string> search(HttpContext context) {
         CustomerModel[] listOfCustomers = await this.fetchAllCustomers();
         CustomerSearchQuery query = await Deserilise<CustomerSearchQuery>(context);
         CustomerModel customer = Array.Find<CustomerModel>(listOfCustomers, customer => customer.MOBILE == query.QUERY || customer.AADHAAR == query.QUERY);

         if (customer == null) {
            return "Customer Not Found";
         }

         return Serialize<CustomerModel>(customer);
      }
   }
}