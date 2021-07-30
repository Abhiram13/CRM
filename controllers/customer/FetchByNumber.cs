using System;
using Models;
using System.Threading.Tasks;

namespace CustomerManagement {
   public sealed partial class CustomerController {
      public static async Task<Customer> FetchByNumber(long number) {
         Customer[] customers = await FetchAllCustomers();
         return Array.Find<Customer>(customers, customer => customer.mobile == number || customer.aadhaar == number);
      }
   }
}