using System.Threading.Tasks;
using BlazorEasyAuth.Extensions;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Handlers
{
    internal class RelativeRoleByUserRequirementHandler : AuthorizationHandler<RelativeRoleRequirement, IUser>
    {
        private readonly IRoleRequirementTestService _roleRequirementTestService;

        public RelativeRoleByUserRequirementHandler(IRoleRequirementTestService roleRequirementTestService)
        {
            _roleRequirementTestService = roleRequirementTestService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RelativeRoleRequirement requirement, IUser resource)
        {
            var userHighestRole = context.User.GetHighestRole();
            var targetUserHighestRole = resource.GetHighestRole();

            if (_roleRequirementTestService.TestRequirement(targetUserHighestRole, requirement.RoleComparison, userHighestRole))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}