using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
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
			response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000/");
			base.OnActionExecuting(context);
		}
	}

	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class RoleAuthoriseAttribute : ActionFilterAttribute {
		private string[] roles = new string[] { };
		public RoleAuthoriseAttribute() {}
      public RoleAuthoriseAttribute(string[] Roles) {
			roles = Roles;
		}

		public override void OnActionExecuting(ActionExecutingContext context) {
         try {
				//Fetch logged employee details by providing cookie through HttpRequest
				EmployeeResponseBody loggedInEmployee = CookieManagement.Fetch(context.HttpContext.Request);

				//check if employee role exists in given roles array
				string isRoleExist = Array.Find<string>(roles, role => role == loggedInEmployee.role);

				//should specific roles be authorised or all roles are authorised?
				string role = roles.Length > 0 ? isRoleExist : "all";

				// Is employee exist? returns false, if not found
				bool employeeExist = loggedInEmployee != null;

				// if employee do not exist, or role do not match
				if (employeeExist == false || role == null) {
					UnauthorizedObjectResult result = new UnauthorizedObjectResult("You are unauthorised to access this route") {
						StatusCode = 401,
					};
					context.Result = result;
				}
				base.OnActionExecuting(context);
         } catch (InvalidCookieException error) {
				UnauthorizedObjectResult result = new UnauthorizedObjectResult(error.Message) {
					StatusCode = 401,
				};
				context.Result = result;
         }
		}
	}
}