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
			
			using var sha256 = SHA256.Create();
			var sha256data = sha256.ComputeHash(Encoding.UTF8.GetBytes(@string));

			return sha256data;
		}
	}
}
