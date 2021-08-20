using System;
using Models;
using CRM;
using Services.Security;
using DataBase;

namespace Services {
	namespace Authentication {
		public class Login : DatabaseOperations<Employee> {
			private LoginRequest _login { get; set; }
			private Employee employee { get; set; }
			public Login(DocumentStructure<Employee> document, LoginRequest login) : base(document) {
				_login = login;
				employee = FetchOne()[0];
			}

			private bool IsPasswordValid() {
				return Hash.Compare(employee.salt, _login.password, employee.password);
			}

			public ResponseModel Authenticate() {
				bool passwordCheck = this.IsPasswordValid();
				if (employee == null) {
					return new ResponseModel(StatusCode.NotFound);
				} else if (passwordCheck == false) {
					return new ResponseModel(StatusCode.Forbidden);
				}

				return new ResponseModel(StatusCode.OK, Text.Encode($"{_login.empid.ToString()}_{_login.password.ToString()}"));
			}
		}
		// public class Login : Services<Employee> {
		//    private Employee emp;
		//    public Login(HttpRequest request) : base(request, Table.employee) { }

		//    private Employee employee() {
		//       FilterDefinition<Employee> filter = document.builders.Eq("empid", requestBody.empid);
		//       return document.FetchOne(filter);
		//    }

		//    private bool IsPasswordValid() {
		//       return Hash.Compare(emp.salt, requestBody.password, emp.password);
		//    }

		//    public ResponseBody<string> Authenticate() {
		//       ResponseBody<string> response = new ResponseBody<string>();
		//       emp = this.employee();

		//       if (emp == null) {
		//          response.body = "Employee does not exist";
		//          response.statusCode = 404;
		//          return response;
		//       }

		//       bool passwordCheck = this.IsPasswordValid();

		//       if (passwordCheck == false) {
		//          response.body = "password is incorrect";
		//          response.statusCode = 401;
		//          return response;
		//       }

		//       response.body = Text.Encode($"{requestBody.empid.ToString()}_{requestBody.password.ToString()}");
		//       response.statusCode = 200;

		//       return response;
		//    }
		// }
	}
}