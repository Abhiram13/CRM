using System;
using Models;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;

namespace Services {
	namespace Security {
		public static class Hash {
			private static string HashPassword(byte[] salt, string password) {
				return Convert.ToBase64String(KeyDerivation.Pbkdf2(
			      password: password,
			      salt: salt,
		         prf: KeyDerivationPrf.HMACSHA256,
		         iterationCount: 100000,
		         numBytesRequested: 256 / 8)
            );
			}

			public static HashDetails GenerateHashedPassword(string password) {
				byte[] salt = new byte[128 / 8];
				using (var rngCsp = new RNGCryptoServiceProvider()) {
					rngCsp.GetNonZeroBytes(salt);
				}
				string hashed = HashPassword(salt, password);
				return new HashDetails() {
					password = hashed,
					salt = Convert.ToBase64String(salt),
				};
			}

			public static string CreateHashPasswordfromSalt(string salt, string password) {
				return HashPassword(Convert.FromBase64String(salt), password);
			}

			public static bool Compare(string salt, string password, string dbPassword) {
				string hashedPassword = CreateHashPasswordfromSalt(salt, password);				
				return hashedPassword == dbPassword;
			}
		}

	// 	Console.Write("Enter a password: ");
	// 			string password = Console.ReadLine();

	// 	// generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
	// 	byte[] salt = new byte[128 / 8];
	// 			using (var rngCsp = new RNGCryptoServiceProvider()) {
	// 				rngCsp.GetNonZeroBytes(salt);
	// 			}
	// string sal = Convert.ToBase64String(salt);
	// byte[] b = Convert.FromBase64String(sal);

	// // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
	// string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
	//     password: password,
	//     salt: salt,
	//     prf: KeyDerivationPrf.HMACSHA256,
	//     iterationCount: 100000,
	//     numBytesRequested: 256 / 8));
	// Console.WriteLine($"Hashed: {hashed}");
	}
}