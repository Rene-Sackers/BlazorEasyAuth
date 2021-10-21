using System.Security.Claims;

namespace BlazorEasyAuth.Extensions
{
	public static class ClaimsIdentityExtensions
	{
		public static string GetId(this ClaimsIdentity claimsIdentity)
		{
			if (!claimsIdentity.IsAuthenticated)
				return null;

			var sidClaim = claimsIdentity.FindFirst(ClaimTypes.Sid);
			return sidClaim?.Value;
		}
		
		public static string GetId(this ClaimsPrincipal claimsPrincipal)
		{
			var sidClaim = claimsPrincipal.FindFirst(ClaimTypes.Sid);
			return sidClaim?.Value;
		}
	}
}