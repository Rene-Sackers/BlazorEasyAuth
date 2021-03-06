using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Extensions
{
	public static class UserRolesExtensions
	{
		public static Role GetHighestRole(this ClaimsPrincipal claimsPrincipal)
			=> claimsPrincipal
				.Claims
				.Where(c => c.Type == ClaimTypes.Role)
				.Select(c => Role.AllRoles.FirstOrDefault(r => r == c.Value))
				.GetHighestRole();

		public static Role GetHighestRole(this IUser user)
			=> user
				.Roles
				.Select(r => Role.AllRoles.FirstOrDefault(role => role == r))
				.GetHighestRole();

		public static Role GetHighestRole(this IEnumerable<Role> roles)
			=> roles
				?.Where(r => r != null)
				.OrderByDescending(r => r.Priority)
				.FirstOrDefault();
	}
}
