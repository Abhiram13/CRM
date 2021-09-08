using System;
using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Services {
	namespace Security {
		public static class Hash {
			private static string ConvertPassword(byte[] salt, string password) {
				byte[] keyBinary = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA256, 100000, 256 / 8);
				return Convert.ToBase64String(keyBinary);
			}

			public static HashDetails GenerateHashedPassword(string password) {
				byte[] salt = new byte[128 / 8];
				using (var rngCsp = new RNGCryptoServiceProvider()) {
					rngCsp.GetNonZeroBytes(salt);
				}
				string hashed = ConvertPassword(salt, password);
				return new HashDetails() {
					password = hashed,
					salt = Encoding.ASCII.GetString(salt),
				};
			}

			public static string CreateHashPasswordfromSalt(string salt, string password) {
				return ConvertPassword(Encoding.ASCII.GetBytes(salt), password);
			}

			public static bool Compare(string salt, string password, string dbPassword) {
				string hashedPassword = CreateHashPasswordfromSalt(salt, password);
				Console.WriteLine(salt);
				Console.WriteLine(password);
				Console.WriteLine(hashedPassword);
				Console.WriteLine(dbPassword);
				return hashedPassword == dbPassword;
			}
		}
	}
}