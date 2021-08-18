using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using Services.BranchManagement;

namespace Controllers {
   namespace BranchManagement {
      [Route("branch")]
      [RoleAuthorise]
      [ResponseHeaders]
      public class BranchController : Controller {
         [HttpPost]
         [Route("add")]
			public ResponseModel Add() {
				Branch branch = RequestBody.Decode<Branch>(Request);
				DocumentStructure<Branch> obj = new DocumentStructure<Branch>() {
					Collection = Table.branch,
					RequestBody = branch,
					filter = Builders<Branch>.Filter.Eq("branch", branch.branch) & Builders<Branch>.Filter.Eq("location", branch.location),
				};
				return new BranchService(obj).Insert();
			}

			[HttpGet]
			[Route("all")]
			public List<BranchResponseModel> All() {
            DocumentStructure<Branch> document = new DocumentStructure<Branch>() { Collection = Table.branch };
				return new BranchService(document).FetchAll();
			}
      }
   }
}