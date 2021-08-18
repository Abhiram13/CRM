using System;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Services.Authentication;

namespace Controllers {
   namespace Authentication {
      [Route("")]
      [ResponseHeaders]
      public class AuthenticationController : Controller {

         [HttpGet]
         public ResponseModel Home() {
				return new ResponseModel(System.StatusCode.Inserted, "");
			}

         [HttpPost]
         [Route("Login")]
         public string login() {
            ResponseBody<string> response = new Login(Request).Authenticate();
            CookieOptions options = new CookieOptions() {
               SameSite = SameSiteMode.None,
               Domain = "localhost",
               Secure = true,
            };
            // Response.Headers.Add("Access-Control-Allow-Credentials", "true");
            // Response.Headers.Add("Content-Type", "text/plain");
            Response.StatusCode = response.statusCode;
            Response.Cookies.Append("auth", response.body, options);
            return response.body;
         }
      }
   }
}