using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Services.ApiManagement;
using MongoDB.Driver;
using CRM;

namespace Controllers {
   namespace Api {
      [Route("Api")]
		[ResponseHeaders]
      [RoleAuthorise]
      public class ApiController : Controller {
         [HttpGet]
         [Route("States")]
         public ResponseModel<List<string>> States() {
				DocumentStructure<StatesModels> document = new DocumentStructure<StatesModels>() { Collection = Table.states };
				return new ResponseModel<List<string>>(System.StatusCode.OK, new States(document).FetchAll());
			}

			[HttpGet]
			[Route("Roles")]
			public ResponseModel<List<string>> Roles() {
				DocumentStructure<RolesModels> document = new DocumentStructure<RolesModels>() { Collection = Table.roles };
				return new ResponseModel<List<string>>(System.StatusCode.OK, new Roles(document).FetchAll());
			}

         [HttpGet]
         [Route("Locations")]
         public ResponseModel<List<string>> Locations() {
				DocumentStructure<Location> document = new DocumentStructure<Location>() { Collection = Table.location };
				return new ResponseModel<List<string>>(System.StatusCode.OK, new Locations(document).FetchAll());
         }

         [HttpPost]
         [Route("Locations/Add")]
         public ResponseModel AddLocations() {
				Location body = RequestBody.Decode<Location>(Request);
				body.token = Text.Serialize<Location>(body);
				DocumentStructure<Location> document = new DocumentStructure<Location>() {
               Collection = Table.location,
               RequestBody = body,
               filter = Builders<Location>.Filter.Eq("location", body.location),
				};            

				return new Locations(document).Insert();
			}
      }
   }
}