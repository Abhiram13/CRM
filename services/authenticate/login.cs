using System;
using Models;
using CRM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AuthenticationService {
   public partial class Authentication {
      public class Login {
         private LoginRequest request;
         public Login(LoginRequest req) {
            request = req;
         }

         private void isEmployee() {
         }
      }
   }
}