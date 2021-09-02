using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using MongoDB.Driver;
using Services.BranchManagement;
using CRM;
using MongoDB.Bson;

namespace Controllers {
   namespace BranchManagement {
      [Route("Branch")]
      [RoleAuthorise]
      [ResponseHeaders]
      public class BranchController : Controller {
         [HttpPost]
         [Route("Add")]
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
			[Route("All")]
			public ResponseModel<List<BranchResponseModel>> All() {
            DocumentStructure<Branch> document = new DocumentStructure<Branch>() { Collection = Table.branch };
            List<BranchResponseModel> branches = new BranchService(document).FetchAll();
				return new ResponseModel<List<BranchResponseModel>>(System.StatusCode.OK, branches);
			}

			[HttpPut]
			[Route("Update")]
			public ResponseModel Update() {
            try {
					NewBranch branch = RequestBody.Decode<NewBranch>(Request);
					DocumentStructure<NewBranch> document = new DocumentStructure<NewBranch>() {
						Collection = Table.branch,
						filter = Builders<NewBranch>.Filter.Eq("location", branch.location) & Builders<NewBranch>.Filter.Eq("branch", branch.branch),
						update = Builders<NewBranch>.Update.Set("branch", branch.newBranch),
					};

					return new BranchService<NewBranch>(document).UpdateOne();
            } catch (Exception error) {
					return new ResponseModel(System.StatusCode.BadRequest, error.Message);
				}
			}
      }
   }
}