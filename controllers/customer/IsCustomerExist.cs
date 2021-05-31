using System.Threading.Tasks;
using Models;

namespace CustomerManagement {
   public sealed partial class CustomerController {      
      public static async Task<bool> IsCustomerExist(long query) {
         Customer customer = await FetchByNumber(query);
         return customer == null;
      }
   }
}