using System;
using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Services {
   namespace Security {
      public static class Hash {
         private static string ConvertPassword(byte[] salt, string password) {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
               password: password,
               salt: salt,
               prf: KeyDerivationPrf.HMACSHA1,
               iterationCount: 10000,
               numBytesRequested: 256 / 8)
            );
         }

         /// <summary>
         /// Generats hashed password from given password
         /// </summary>
         /// <param name="password">Password need to hashed</param>
         /// <returns>Salt and hashed password</returns>
         public static HashDetails GenerateHashedPassword(string password) {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create()) {
               rng.GetBytes(salt);
            }

            string hashed = ConvertPassword(salt, password);

            return new HashDetails() {
               password = hashed,
               salt = Encoding.ASCII.GetString(salt),
            };
         }

         /// <summary>
         /// Creates password with given salt
         /// </summary>
         /// <param name="salt">Salt code from Database</param>
         /// <param name="password">Password from user input</param>
         /// <returns>Returns hashed password</returns>
         public static string CreateHashPasswordfromSalt(string salt, string password) {
            return ConvertPassword(Encoding.ASCII.GetBytes(salt), password);
         }

         /// <summary>
         /// Compares hashed password with password in database
         /// </summary>
         /// <param name="salt">Salt code from Database</param>
         /// <param name="password">Password from user input</param>
         /// <param name="dbPassword">Password from Database to compare</param>
         /// <returns>Returns boolean based on comparison</returns>
         public static bool Compare(string salt, string password, string dbPassword) {
            string hashedPassword = CreateHashPasswordfromSalt(salt, password);
            return hashedPassword == dbPassword;
         }
      }
   }
}