using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Requirements
{
	public enum RoleRequirement
	{
		Higher,
		Equal,
		LesserOrEqual,
		Lesser
	}
	
	internal class RelativeRoleRequirement : IAuthorizationRequirement
	{
		public RoleRequirement RoleRequirement { get; }

		public RelativeRoleRequirement(RoleRequirement roleRequirement)
		{
			RoleRequirement = roleRequirement;
		}
	}
}
