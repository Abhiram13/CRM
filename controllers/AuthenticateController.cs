using System;
using CRM;
using Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Services.Authentication;

namespace Controllers {
   namespace Authentication {
      [Route("")]
      [ResponseHeaders]
      public class AuthenticationController : Controller {

         [HttpGet]
         public ResponseBody<string> Home() {
            // Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000/");
            return new ResponseBody<string> {
               body = "Hello World",
               statusCode = 200,
            };
         }

         [HttpPost]
         [Route("login")]
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