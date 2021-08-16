using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Services.BranchManagement;

namespace Controllers {
   namespace BranchManagement {
      [Route("branch")]
      [RoleAuthorise]
      [ResponseHeaders]
      public class BranchController : Controller {
         [HttpPost]
         [Route("add")]
         public short Add() {
				return new BranchServices(Request).Insert();
			}

			[HttpGet]
			[Route("all")]
			public List<Branch> All() {
				return new BranchServices(Table.branch).FetchAll();
			}
      }
   }
}