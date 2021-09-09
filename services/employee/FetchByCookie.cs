using System;
using CRM;
using Microsoft.AspNetCore.Http;
using Models;
using Controllers.EmployeeManagement;

namespace Services {
   namespace EmployeeManagement {      
      public class CookieManagement {
         #nullable enable
         public static EmployeeResponseBody Fetch(HttpRequest request) {
				string cookie = request.Headers["Cookie"];
				if (cookie == "" || cookie == null) throw new InvalidCookieException();
				Employee? employee = EmployeeService.GetByToken(cookie);
            if (employee == null) throw new InvalidCookieException();
				return new EmployeeController().FetchOne(employee.empid).Response;
			}
      }
   }
}