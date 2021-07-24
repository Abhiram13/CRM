using System;
using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace AuthenticationService {
   public static class HashPassword {

      /// <summary>
      /// Hashes given password and returns it's salt and password
      /// </summary>
      /// <param name="password">Password need to hashed</param>
      /// <returns>Salt and hashed password</returns>
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
            salt = Encoding.ASCII.GetString(salt),
         };
      }

      /// <summary>
      /// Hashes password with given/ already existed salt
      /// </summary>
      /// <param name="salt">Salt code from Database</param>
      /// <param name="password">Password from user input</param>
      /// <returns>Returns hashed password to compare with database password</returns>
      public static string compareHash(string salt, string password) {         
         return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: Encoding.ASCII.GetBytes(salt),
            prf: KeyDerivationPrf.HMACSHA1,
            iterationCount: 10000,
            numBytesRequested: 256 / 8)
         );
      }
   }
}