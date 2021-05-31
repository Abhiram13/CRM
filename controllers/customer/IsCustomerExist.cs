using System;
using System.Threading.Tasks;
using Models;
using CRM;

namespace CustomerManagement {
   public sealed partial class CustomerController {
      // private 
      public static async Task<bool> IsCustomerExist(long query) {
         Customer customer = await FetchByNumber(query);
         return customer == null;
      }
   }
}