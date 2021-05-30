using System.Threading.Tasks;
using Models;
using CRM;

namespace EmployeeManagement {
   public sealed partial class EmployeeController {
      public static async Task<Employee[]> FetchAllEmployees() {
         string employee = await new Database<Employee>("employee").FetchAll();
         return JSONObject.DeserializeObject<Employee[]>(employee);         
      }
   }
}