using System;

namespace BlazorEasyAuth.Models
{
	internal class SignInToken
	{
		public string UserId { get; set; }

		public string Token { get; set; }

		public DateTimeOffset ExpirationDate { get; set; }
	}
}
