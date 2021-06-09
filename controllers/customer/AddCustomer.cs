using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;

namespace CustomerManagement {
   public sealed partial class CustomerController : Controller {
      private Task<Customer> customer;

      public CustomerController(HttpContext Context) {
         this.customer = JSONObject.Deserilise<Customer>(Context);
      }

      public async Task<string> Add() {
         long query = (long)(await this.customer).MOBILE;

         DocumentVerification<Customer> details = new DocumentVerification<Customer>() {
            boolean = !(await IsCustomerExist(query)),
            table = Table.customer,
            document = await this.customer,
         };

         return Add<Customer>(details);
      }
   }
}