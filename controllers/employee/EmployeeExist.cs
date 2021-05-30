using System.Threading.Tasks;
using Models;
using CRM;

namespace EmployeeManagement {
   public sealed partial class EmployeeController {
      public static async Task<bool> IsEmployeeExist(int employeeId) {
         Employee employee = await EmployeeController.FetchById(employeeId.ToString());
         return employee == null;
      }
   }
}