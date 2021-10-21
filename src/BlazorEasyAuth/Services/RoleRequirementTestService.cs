using System;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;
using BlazorEasyAuth.Services.Interfaces;

namespace BlazorEasyAuth.Services
{
	public class RoleRequirementTestService : IRoleRequirementTestService
	{
		public bool TestRequirement(Role roleA, RoleComparison comparison, Role roleB)
		{
			return comparison switch
			{
				RoleComparison.Higher => roleA > roleB,
				RoleComparison.HigherOrEqual => roleA >= roleB,
				RoleComparison.Equal => roleA == roleB,
				RoleComparison.LesserOrEqual => roleA <= roleB,
				RoleComparison.Lesser => roleA < roleB,
				_ => throw new ArgumentOutOfRangeException(nameof(comparison))
			};
		}
	}
}
