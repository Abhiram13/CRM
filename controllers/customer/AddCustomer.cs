using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;

namespace CustomerManagement {
   public sealed partial class CustomerController {
      private Task<Customer> customer;

      public CustomerController(HttpContext Context) : base(Context) {
         this.customer = JSONObject.Deserilise<Customer>(Context);
      }

      public async Task<string> Add() {
         long query = (long)(await this.customer).MOBILE;
         bool isCustomerExist = await IsCustomerExist(query);

         return Add<Customer>(isCustomerExist, Table.customer, await this.customer);
      }
   }
}