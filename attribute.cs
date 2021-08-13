using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace System {
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class ResponseHeadersAttribute : ActionFilterAttribute {
		// public override void OnResultExecuting(ResultExecutingContext context) {
		// 	HttpResponse response = context.HttpContext.Response;
		// 	// string jsonString = "Response already sent";
		// 	// byte[] data = Encoding.UTF8.GetBytes(jsonString);
		// 	response.Headers.Add("Access-Control-Allow-Credentials", "true");
		// 	// response.Body.WriteAsync(data, 0, data.Length);
		// 	base.OnResultExecuting(context);
		// }

		public override void OnActionExecuting(ActionExecutingContext context) {
			HttpResponse response = context.HttpContext.Response;
			response.Headers.Add("Access-Control-Allow-Credentials", "true");
			UnauthorizedObjectResult result = new UnauthorizedObjectResult("asdgasdasgd") {
				StatusCode = 200,
			};

			context.Result = result;
			base.OnActionExecuting(context);
		}
	}
}