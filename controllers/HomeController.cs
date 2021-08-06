using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Controllers {
   namespace Api {
      [Route("api")]
      public class ApiController : Controller {

         [HttpGet]
         [Route("states")]
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