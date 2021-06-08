using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IRoleRequirementTestService
	{
		bool TestRequirement(Role baseRole, Role targetRole, RoleRequirement targetRoleRequirement);
	}
}