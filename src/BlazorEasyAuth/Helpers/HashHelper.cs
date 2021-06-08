using System;
using System.Security.Cryptography;
using System.Text;

namespace BlazorEasyAuth.Helpers
{
	public static class HashHelper
	{
		public static byte[] GetPasswordHash(string @string)
		{
			if (string.IsNullOrWhiteSpace(@string))
				throw new ArgumentException("Can't hash empty string", nameof(@string));
			
			var sha256 = new SHA256CryptoServiceProvider();
			var sha256data = sha256.ComputeHash(Encoding.UTF8.GetBytes(@string));

			return sha256data;
		}
	}
}
