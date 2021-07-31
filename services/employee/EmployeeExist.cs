using System.Threading.Tasks;
using Models;
using CRM;

namespace EmployeeManagement {
   public sealed partial class EmployeeService {
      public static bool checkEmployee(int employeeId) {
         Employee emp = EmployeeService.FetchById(employeeId);
         return emp == null;
      }
   }
}