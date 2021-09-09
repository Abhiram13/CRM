using Models;
using System;
using MongoDB.Driver;
using MongoDB.Bson;
using DataBase;
using System.Collections.Generic;

namespace Services {
	namespace EmployeeManagement {
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

         #nullable enable
			public static Employee? GetByToken(string token) {
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {
					Collection = Table.employee,
					filter = Builders<Employee>.Filter.Eq("token", token),
				};
				List<Employee> list = new EmployeeService(document).FetchOne();
				return list.Count > 0 ? list[0] : null;
			}
		}
	}
}