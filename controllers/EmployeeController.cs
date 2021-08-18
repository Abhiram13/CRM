using Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using MongoDB.Bson;
using Services.Security;
using MongoDB.Driver;
using Services.EmployeeManagement;

namespace Controllers {
   namespace EmployeeManagement {
      [Route("employee")]
      [ResponseHeaders]
      public partial class EmployeeController : Controller {
 
         // [HttpGet]
         // [Route("fetchByCookie")]
         // public EmployeeResponseBody fetch() {
         //    return EmployeeService.fetchByCookie(Request);
         // }

         [HttpPost]
         [Route("add")]
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
         [Route("all")]
         public List<EmployeeResponseBody> FetchAll() {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {Collection = Table.employee};
				return new EmployeeService(document).FetchAll();
			}

			[HttpGet]
			[Route("fetch/{id}")]
			public EmployeeResponseBody FetchOne(int id) {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() { 
               Collection = Table.employee,
               filter = Builders<Employee>.Filter.Eq("empid", id),
            };
				return new EmployeeResponseBody(new EmployeeService(document).FetchOne()[0]);
			}
      }
   }
}