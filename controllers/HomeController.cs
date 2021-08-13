using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services.Security;
using System;

namespace Controllers {
   namespace Api {
      [Route("api")]
      public class ApiController : Controller {

         // [HttpGet]
         // [Route("")]
         // public void test() {
			// 	Crypto.hash();
			// }

         [HttpGet]
         [Route("states")]
         [ResponseHeaders]
         public string[] States() {
            return Services.ApiManagement.States.fetchStates();
         }

         [HttpGet]
         [Route("roles")]
         public string[] Roles() {
            return Services.ApiManagement.Roles.fetchRoles();
         }

         [HttpGet]
         [Route("locations")]
         public string[] Locations() {
            return Services.ApiManagement.Locations.fetchLocations();
         }
      }
   }
}