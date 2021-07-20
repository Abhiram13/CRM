using System;
using CRM;
using Models;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace EmployeeManagement {
   public partial class EmployeeService {
      public static void fetchByCookie(HttpRequest request) {
         string cookie = request.Headers["Cookie"];
         Console.WriteLine(cookie);
         string str = cookie.Split("=")[1];
         string replaced = str.Replace("%3D", "=");
         Console.WriteLine(Text.Decode(replaced));
      }
   }
}