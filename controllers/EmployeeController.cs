using Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MongoDB.Bson;
using Services.EmployeeManagement;

namespace Controllers {
   namespace EmployeeManagement {
      [Route("employee")]
      [ResponseHeaders]
      public partial class EmployeeController : Controller {
 
         [HttpGet]
         [Route("fetchByCookie")]
         public EmployeeResponseBody fetch() {
            return EmployeeService.fetchByCookie(Request);
         }

         [HttpPost]
         [Route("add")]
         public void Add() {
            short code = new EmployeeService(Request).Insert();
            Response.StatusCode = code;
         }

         [HttpGet]
         [Route("all")]
         public List<EmployeeResponseBody> FetchAll() {
            return EmployeeService.FetchAllEmployees(Request);
         }
      }
   }
}