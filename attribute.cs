using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace System {
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class ResponseHeadersAttribute : ActionFilterAttribute {
		public override void OnActionExecuting(ActionExecutingContext context) {
			HttpResponse response = context.HttpContext.Response;
			response.Headers.Add("Access-Control-Allow-Credentials", "true");			
			// UnauthorizedObjectResult result = new UnauthorizedObjectResult("asdgasdasgd") {
			// 	StatusCode = 200,
			// };

			// context.Result = result;
			base.OnActionExecuting(context);
		}
	}
}