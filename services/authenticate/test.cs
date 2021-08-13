using System;
using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Services {
   namespace Security {
      public static class Crypto {
         public static void hash() {
				HMACSHA256 hm = new HMACSHA256();
				Console.WriteLine(hm);
			}
      }
   }
}