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
         public ResponseModel<List<EmployeeResponseBody>> FetchAll() {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {Collection = Table.employee};
				return new ResponseModel<List<EmployeeResponseBody>>(System.StatusCode.OK, new EmployeeService(document).FetchAll());
			}

			[HttpGet]
			[Route("Fetch/{id}")]
			public ResponseModel<EmployeeResponseBody> FetchOne(int id) {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() { 
               Collection = Table.employee,
               filter = Builders<Employee>.Filter.Eq("empid", id),
            };
				EmployeeResponseBody response = new EmployeeResponseBody(new EmployeeService(document).FetchOne()[0]);
				return new ResponseModel<EmployeeResponseBody>(System.StatusCode.OK, response);
			}

			[HttpGet]
			[Route("FetchY/{id}")]
			public ResponseModel<Employee> FetchOneY(int id) {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {
					Collection = Table.employee,
					filter = Builders<Employee>.Filter.Eq(emp => emp.empid, id),
               // project = Builders<Employee>.Projection.Exclude(emp => emp.firstname),            
				};
				Employee response = new EmployeeService(document).Test()[0];
				return new ResponseModel<Employee>(System.StatusCode.OK, response);
			}

			[HttpGet]
			[Route("FetchByCookie")]
			public ResponseModel<EmployeeResponseBody> FetchByCookie() {			
				return new ResponseModel<EmployeeResponseBody>(System.StatusCode.OK, CookieManagement.Fetch(Request));
			}
      }
   }
}