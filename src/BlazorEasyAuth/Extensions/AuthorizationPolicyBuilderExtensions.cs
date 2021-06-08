using BlazorEasyAuth.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Extensions
{
	public static class AuthorizationPolicyBuilderExtensions
	{
		public static AuthorizationPolicyBuilder AddRelativeRoleRequirement(this AuthorizationPolicyBuilder builder, RoleRequirement requirement)
			=> builder.AddRequirements(new RelativeRoleRequirement(requirement));
	}
}