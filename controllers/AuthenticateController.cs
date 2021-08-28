using System;
using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Services.Authentication;
using MongoDB.Driver;

namespace Controllers {
   namespace Authentication {
      [Route("")]
      public class AuthenticationController : Controller {

         [HttpGet]
         public ResponseModel Home() {
				return new ResponseModel(System.StatusCode.Inserted, "");
			}

         [HttpPost]
         [Route("Login")]
         public ResponseModel login() {
				LoginRequest login = RequestBody.Decode<LoginRequest>(Request);
				DocumentStructure<Employee> document = new DocumentStructure<Employee>() {
					Collection = Table.employee,
					filter = Builders<Employee>.Filter.Eq("empid", login.empid),
				};
				ResponseModel response = new Login(document, login).Authenticate();
				CookieOptions options = new CookieOptions() {
				   SameSite = SameSiteMode.None,
				   Domain = "localhost",
				   Secure = true,               
				};
				Response.Headers.Add("Access-Control-Allow-Credentials", "true");
				Response.Cookies.Append("auth", response.Response, options);		

				return response;
			}
      }
   }
}