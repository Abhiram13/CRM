using Models;

namespace EmployeeManagement {
   public sealed partial class EmployeeService {
      public static bool isEmployeeExist(int employeeId) {
         Employee emp = EmployeeService.FetchById(employeeId);
         return emp == null;
      }
   }
}