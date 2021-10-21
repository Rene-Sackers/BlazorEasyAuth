using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IRoleRequirementTestService
	{
		bool TestRequirement(Role roleA, RoleComparison comparison, Role roleB);
	}
}