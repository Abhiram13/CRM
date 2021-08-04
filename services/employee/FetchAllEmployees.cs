using Models;
using CRM;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Services {
   namespace EmployeeManagement {
      public partial class EmployeeService : Services<Employee> {
         public List<EmployeeResponseBody> FetchAllEmployees(HttpRequest request) {
            List<Employee> employeesList = document.FetchAll();
            List<EmployeeResponseBody> response = new List<EmployeeResponseBody>();

            foreach (Employee employee in employeesList) {
               response.Add(new EmployeeResponseBody() {
                  branch = employee.branch,
                  email = employee.email,
                  empid = employee.empid,
                  firstname = employee.firstname,
                  lastname = employee.lastname,
                  location = employee.location,
                  mobile = employee.mobile,
                  role = employee.role,
                  state = employee.state,
                  title = employee.title,
               });
            }

            return response;
         }
      }
   }
}