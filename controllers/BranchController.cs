using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Controllers {
   namespace BranchManagement {
      [Route("branch")]
      public class BranchController : Controller {
         [HttpPost]
         [Route("add")]
         public ObjectResult Add() {
            return StatusCode(300, new ResponseBody<string>() { body = "asdasdh" });
         }
      }
   }
}