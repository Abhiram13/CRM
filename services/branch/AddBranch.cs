using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Models;
using CRM;
using System;
using AuthenticationService;
using Database;

namespace BranchManagement {
   public sealed partial class BranchServices {
      public async void Add(HttpRequest request) {
         Branch branch = await JSONN.httpContextDeseriliser<Branch>(request);
         // DatabaseService<>
      }
   }
}