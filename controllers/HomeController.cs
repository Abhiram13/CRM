using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ApiManagement {
   [Route("api")]
   public class ApiController : Microsoft.AspNetCore.Mvc.Controller {

      [HttpGet]
      [Route("states")]
      public string[] States() {
         return ApiServices.fetchStates();
      }

      [HttpGet]
      [Route("roles")]
      public string[] Roles() {
         return ApiServices.fetchRoles();
      }

      [HttpGet]
      [Route("locations")]
      public string[] Locations() {
         return ApiServices.fetchLocations();
      }
   }
}