using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Extensions
{
	public static class AuthorizationPolicyBuilderExtensions
	{
		public static AuthorizationPolicyBuilder AddRelativeRoleRequirement(this AuthorizationPolicyBuilder builder, RoleComparison comparison)
			=> builder.AddRequirements(new RelativeRoleRequirement(comparison));
	}
}