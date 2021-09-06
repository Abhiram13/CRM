using Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;
using Services.Security;
using MongoDB.Driver;
using Services.EmployeeManagement;
using CRM;

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
				employee.token = Text.Tokenize<int, long>(employee.empid, employee.mobile);
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
				List<Employee> list = new EmployeeService(document).FetchOne();
				Employee employee = list.Count > 0 ? list[0] : new Employee();
				EmployeeResponseBody response = new EmployeeResponseBody(employee);
				return new ResponseModel<EmployeeResponseBody>(System.StatusCode.OK, response);
			}

			[HttpGet]
			[Route("FetchByCookie")]
			public ResponseModel<EmployeeResponseBody> FetchByCookie() {
				return new ResponseModel<EmployeeResponseBody>(System.StatusCode.OK, CookieManagement.Fetch(Request));
			}

         // [HttpPut]
         // [Route("Update")]
         // public void Update() {
			// 	Employee employee = RequestBody.Decode<Employee>(Request);
			// 	UpdateDefinition<Employee> update;

			// 	foreach(var key in employee.GetType().GetProperties()) {
			// 		var k = key.Name;
			// 		var v = key.GetValue(employee);
			// 		if (v != null || k != "_id" || k != "__v") {
			// 			update = Builders<Employee>.Update.Set(k, (string)v);
			// 		}
			// 	}
			// }
      }
   }
}