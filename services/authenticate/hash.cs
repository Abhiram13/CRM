using System;
using CRM;
using Models;
using Microsoft.AspNetCore.Mvc;
using AuthenticationService;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace AuthenticationService {
   public static class HashPassword {
      public static HashDetails hash(string password) {
         byte[] salt = new byte[128 / 8];
         using (var rng = RandomNumberGenerator.Create()) {
            rng.GetBytes(salt);
         }

         string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8)
         );

         return new HashDetails() {
            password = hashed,
            salt = Convert.ToBase64String(salt),
         };
      }
   }
}