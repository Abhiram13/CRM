using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using Services.DatabaseManagement;
using Services.Security;
using DataBase;
using System.Collections.Generic;

namespace Services {
	namespace EmployeeManagement {
		// public partial class EmployeeService : Services<Employee> {
		// 	public EmployeeService(HttpRequest request) : base(request, Table.employee) { }

		// 	public short Insert() {
		// 		FilterDefinition<Employee> filter = document.builders.Eq("empid", requestBody.empid);
		// 		HashDetails hash = Hash.GenerateHashedPassword(requestBody.password);
		// 		requestBody.salt = hash.salt;
		// 		requestBody.password = hash.password;
		// 		return document.Insert(requestBody, filter);
		// 	}
		// }

      public class EmployeeService : DatabaseOperations<Employee>, IFetchAll<EmployeeResponseBody> {
			public EmployeeService(DocumentStructure<Employee> document) : base(document) { }

         public List<EmployeeResponseBody> FetchAll() {
				List<Employee> list = _collection.Find(new BsonDocument()).ToList();
				List<EmployeeResponseBody> listOfEmployees = new List<EmployeeResponseBody>();

            foreach(Employee employee in list) {
					listOfEmployees.Add(new EmployeeResponseBody(employee));
				}

				return listOfEmployees;
			}
		}
	}
}