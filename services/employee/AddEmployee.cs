using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using MongoDB.Driver;
using Services.DatabaseManagement;
using Services.Security;

namespace Services {
	namespace EmployeeManagement {
		public partial class EmployeeService : Services<Employee> {
			public EmployeeService(HttpRequest request) : base(request, Table.employee) { }

			public short Insert() {
				FilterDefinition<Employee> filter = document.builders.Eq("empid", requestBody.empid);
				HashDetails hash = Hash.GenerateHashedPassword(requestBody.password);
				requestBody.salt = hash.salt;
				requestBody.password = hash.password;
				return document.Insert(requestBody, filter);
			}
		}
	}
}