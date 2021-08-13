using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Models;
using Services.EmployeeManagement;

namespace System {
   public class InvalidCookieException : Exception {
		public override string Message { get { return "Provided Cookies are invalid"; } }
	}

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class ResponseHeadersAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext context) {
			HttpResponse response = context.HttpContext.Response;
			Employee employee = EmployeeService.fetchByCookie(context.HttpContext.Request);
			Console.WriteLine(employee.empid);
			response.Headers.Add("Access-Control-Allow-Credentials", "true");			
			// UnauthorizedObjectResult result = new UnauthorizedObjectResult("asdgasdasgd") {
			// 	StatusCode = 200,
			// };

			// context.Result = result;
			base.OnActionExecuting(context);
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class RoleAuthoriseAttribute : ActionFilterAttribute {
		private string[] roles;
      public RoleAuthoriseAttribute(string[] Roles) {
			roles = Roles;
		}
	}
}