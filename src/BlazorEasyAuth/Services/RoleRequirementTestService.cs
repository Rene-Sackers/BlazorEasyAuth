using System;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;
using BlazorEasyAuth.Services.Interfaces;

namespace BlazorEasyAuth.Services
{
	public class RoleRequirementTestService : IRoleRequirementTestService
	{
		public bool TestRequirement(Role baseRole, Role targetRole, RoleRequirement targetRoleRequirement)
		{
			if (targetRole == null)
				return true;

			return targetRoleRequirement switch
			{
				RoleRequirement.Higher => targetRole > baseRole,
				RoleRequirement.Equal => targetRole == baseRole,
				RoleRequirement.LesserOrEqual => targetRole <= baseRole,
				RoleRequirement.Lesser => targetRole < baseRole,
				_ => throw new ArgumentOutOfRangeException(nameof(targetRoleRequirement))
			};
		}
	}
}
