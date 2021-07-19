using System;
using CRM;
using Models;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Authentication {
   [Route("")]
   public class AuthenticationController : Microsoft.AspNetCore.Mvc.Controller {

      [HttpGet]
      public ResponseBody<string> Home() {
         Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:3000/");
         return new ResponseBody<string> {
            body = "Hello World",
            statusCode = 200,
         };
      }

      [HttpPost]
      [Route("login")]
      public async Task<string> Login() {
         LoginRequest request = await JSONN.httpContextDeseriliser<LoginRequest>(Request);
         ResponseBody<string> response = new Authenticate.Login(request).authenticate();
         CookieOptions options = new CookieOptions() {
            SameSite = SameSiteMode.None,
            Domain = "localhost",
            Secure = true,
         };
         Response.Headers.Add("Access-Control-Allow-Credentials", "true");
         Response.StatusCode = response.statusCode;
         Response.Cookies.Append("auth", response.body, options);
         return response.body;
      }

      [HttpGet]
      [Route("/demo")]
      public string Demo() {
         Response.Headers.Add("Access-Control-Allow-Credentials", "true");
         Console.WriteLine(Request.Headers["Cookie"]);
         string password = "123";

         // generate a 128-bit salt using a secure PRNG
         // byte[] salt = new byte[128 / 8];
         // using (var rng = RandomNumberGenerator.Create()) {
         //    rng.GetBytes(salt);
         // }

         // Console.WriteLine($"Password: {password}");
         byte[] b = Encoding.ASCII.GetBytes("S2dQVkVZdXVOSnpPUEZrTWczT2NVdz09");
         string s = Encoding.ASCII.GetString(b);
         Console.WriteLine($"Salt: {s}");
         Console.WriteLine(password);

         // Password: 123
         // Salt: l/1c/u2D/dgXzmK/WeGThw==
         // Hashed: j+vNGaoGEBrIsKnBdLEYmvNUeuxTdH5B0VVEJhNctx0=

         string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: b,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
         Console.WriteLine($"Hashed: {hashed}");
         return $"Hashed: {hashed}";
      }
   }
}