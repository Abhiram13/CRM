using Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MongoDB.Bson;

namespace EmployeeManagement {
   [Route("employee")]
   public partial class EmployeeController : Microsoft.AspNetCore.Mvc.Controller {

      [HttpGet]
      [Route("fetchByCookie")]
      public Employee fetch() {
         Response.Headers.Add("Access-Control-Allow-Credentials", "true");
         return EmployeeService.fetchByCookie(Request);
      }

      [HttpPost]
      [Route("add")]
      public async Task<ResponseBody<string>> Add() {
         return await EmployeeService.Add(Request);
      }

      [HttpGet]
      [Route("all")]
      public List<EmployeeResponseBody> FetchAll() {
         return EmployeeService.FetchAllEmployees(Request);
      }
   }
}