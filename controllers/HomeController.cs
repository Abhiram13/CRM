using System;
using Microsoft.AspNetCore.Mvc;

namespace CRM {
   [Route("")]
   public class HomeController {
      [HttpGet]
      public string home() => "Hello World";
   }
}