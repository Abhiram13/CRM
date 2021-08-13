using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace System {
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
   public class ResponseHeadersAttribute : ActionFilterAttribute {
		public override void OnResultExecuting(ResultExecutingContext context) {
			context.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
			base.OnResultExecuting(context);
		}
	}
}