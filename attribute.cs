using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using Models;
using Services.EmployeeManagement;

namespace System {
   public class InvalidCookieException : Exception {
		public override string Message { get { return "Please provide valid cookies"; } }
	}

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class ResponseHeadersAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext context) {
			HttpResponse response = context.HttpContext.Response;						
			response.Headers.Add("Access-Control-Allow-Credentials", "true");			
			base.OnActionExecuting(context);
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class RoleAuthoriseAttribute : ActionFilterAttribute {
		private string[] roles;
      public RoleAuthoriseAttribute(string[] Roles) {
			roles = Roles;
		}

		public override void OnActionExecuting(ActionExecutingContext context) {
			Employee loggedInEmployee = EmployeeService.fetchByCookie(context.HttpContext.Request);

			// Finds logged in employee role in the array of roles
			// if no role matches, then employee is not authorised
			string isRoleExist = Array.Find<string>(roles, role => role == loggedInEmployee.role);
			if (isRoleExist == null) {
				UnauthorizedObjectResult result = new UnauthorizedObjectResult("You are Unauthorised to access this Route") {
					StatusCode = 401,
				};
				context.Result = result;
			}
			base.OnActionExecuting(context);
		}
	}
}