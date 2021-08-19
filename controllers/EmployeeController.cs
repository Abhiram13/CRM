using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Services.Security;
using MongoDB.Driver;
using Services.EmployeeManagement;

namespace Controllers {
   namespace EmployeeManagement {
      [Route("Employee")]
      [ResponseHeaders]
      [RoleAuthorise]
      public partial class EmployeeController : Controller {
         [HttpPost]
         [Route("Add")]
         public ResponseModel Add() {
				Employee employee = RequestBody.Decode<Employee>(Request);
				HashDetails hash = Hash.GenerateHashedPassword(employee.password);
				employee.salt = hash.salt;
				employee.password = hash.password;
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {
					Collection = Table.employee,
					RequestBody = employee,
					filter = Builders<Employee>.Filter.Eq("empid", employee.empid),
				};
				return new EmployeeService(document).Insert();
			}

         [HttpGet]
         [Route("All")]
         public List<EmployeeResponseBody> FetchAll() {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {Collection = Table.employee};
				return new EmployeeService(document).FetchAll();
			}

			[HttpGet]
			[Route("Fetch/{id}")]
			public EmployeeResponseBody FetchOne(int id) {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() { 
               Collection = Table.employee,
               filter = Builders<Employee>.Filter.Eq("empid", id),
            };
				return new EmployeeResponseBody(new EmployeeService(document).FetchOne()[0]);
			}

			[HttpGet]
			[Route("FetchByCookie")]
			public EmployeeResponseBody FetchByCookie() {
				return CookieManagement.Fetch(Request);
			}
      }
   }
}