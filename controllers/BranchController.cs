using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BranchManagement {
   [Route("api/branch")]
   public class BranchController : Controller {
      [HttpPost]
      [Route("add")]
      public void Add() {}
   }
}