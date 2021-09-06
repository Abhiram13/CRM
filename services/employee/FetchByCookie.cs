using System;
using CRM;
using Microsoft.AspNetCore.Http;
using Models;
using Controllers.EmployeeManagement;

namespace Services {
   namespace EmployeeManagement {
      public class CookieManagement {
         public static EmployeeResponseBody Fetch(HttpRequest request) {
				string cookie = request.Headers["Cookie"];
				if (cookie == "" || cookie == null) throw new InvalidCookieException();
				else {
					string str = cookie.Split("=")[1];
					string replaced = str.Replace("%3D", "=");
					string x = Text.Decode(replaced);
					Console.WriteLine(x);
					return new EmployeeController().FetchOne(123).Response;
				}
			}
      }
   }
}