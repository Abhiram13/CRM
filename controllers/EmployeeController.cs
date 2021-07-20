using Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement {
   [Route("employee")]
   public partial class EmployeeController : Microsoft.AspNetCore.Mvc.Controller {

      [HttpGet]
      [Route("fetchByCookie")]
      public Employee fetch() {
         Response.Headers.Add("Access-Control-Allow-Credentials", "true");
         return EmployeeService.fetchByCookie(Request);
      }
   }
}