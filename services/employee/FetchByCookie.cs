using System;
using CRM;
using Microsoft.AspNetCore.Http;
using Models;
using Controllers.EmployeeManagement;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Services {
   namespace EmployeeManagement {
      public class CookieFetch {
         // public static EmployeeResponseBody fetchByCookie(HttpRequest request) {
         //    string cookie = request.Headers["Cookie"];
			// 	if (cookie == "" || cookie == null) throw new InvalidCookieException();
			// 	else {
			// 		string str = cookie.Split("=")[1];
			// 		string replaced = str.Replace("%3D", "=");
			// 		int id = Convert.ToInt32(Text.Decode(replaced).Split("_")[0]);
         //       Employee emp = EmployeeService.FetchById(id);

			// 		return new EmployeeResponseBody(emp);
			// 	}				
         // }
         public static EmployeeResponseBody FetchByCookie(HttpRequest request) {
				string cookie = request.Headers["Cookie"];
				if (cookie == "" || cookie == null) throw new InvalidCookieException();
				else {
					string str = cookie.Split("=")[1];
					string replaced = str.Replace("%3D", "=");
					int id = Convert.ToInt32(Text.Decode(replaced).Split("_")[0]);
					return new EmployeeController().FetchOne(id);
				}
			}
      }
   }
}