using System;
using CRM;
using Models;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace EmployeeManagement {
   [Route("employee")]
   public partial class EmployeeController : Microsoft.AspNetCore.Mvc.Controller {

      [HttpGet]
      [Route("")]
      public void fetch() {
         EmployeeService.fetchByCookie(Request);
      }
   }
}