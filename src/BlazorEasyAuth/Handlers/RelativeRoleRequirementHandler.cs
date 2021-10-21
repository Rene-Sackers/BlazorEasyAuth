using System.Threading.Tasks;
using BlazorEasyAuth.Extensions;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Handlers
{
	public class RelativeRoleRequirementHandler : AuthorizationHandler<RelativeRoleRequirement, Role>
	{
		private readonly IRoleRequirementTestService _roleRequirementTestService;

		public RelativeRoleRequirementHandler(IRoleRequirementTestService roleRequirementTestService)
		{
			_roleRequirementTestService = roleRequirementTestService;
		}

		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RelativeRoleRequirement requirement, Role resource)
		{
			var userHighestRole = context.User.GetHighestRole();

			if (_roleRequirementTestService.TestRequirement(resource, requirement.RoleComparison, userHighestRole))
				context.Succeed(requirement);

			return Task.CompletedTask;
		}
	}
}
