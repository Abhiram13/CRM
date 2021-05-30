using System;
using System.Threading.Tasks;
using Models;
using CRM;

namespace CustomerManagement {
   public sealed partial class CustomerController : Controller {
      public static async Task<Customer[]> FetchAllCustomers() {
         return await FetchAll<Customer>(Table.customer);
      }
   }
}