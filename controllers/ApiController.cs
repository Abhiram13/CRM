using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Services.Security;
using System;

namespace Controllers {
   namespace Api {
      [Route("api")]
		[ResponseHeaders]
      public class ApiController : Controller {
         [HttpGet]
         [Route("states")]         
         public string[] States() {
            return Services.ApiManagement.States.fetchStates();
         }

         [HttpGet]
         [Route("roles")]
         // [RoleAuthorise(new string[] { RoleType.Admin, RoleType.BranchManager })]
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