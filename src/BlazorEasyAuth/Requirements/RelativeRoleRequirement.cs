using BlazorEasyAuth.Models;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Requirements
{
	public class RelativeRoleRequirement : IAuthorizationRequirement
	{
		public RoleComparison RoleComparison { get; }

		public RelativeRoleRequirement(RoleComparison roleComparison)
		{
			RoleComparison = roleComparison;
		}
	}
}
