using System;
using System.Security.Cryptography;

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