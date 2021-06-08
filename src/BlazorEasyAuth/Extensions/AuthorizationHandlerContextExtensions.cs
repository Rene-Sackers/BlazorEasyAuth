using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Extensions
{
	internal static class AuthorizationHandlerContextExtensions
	{
		public static Task SetSuccessAndReturnCompletedTask(this AuthorizationHandlerContext context, IAuthorizationRequirement requirement, bool success)
		{
			if (success)
				context.Succeed(requirement);
			else
				context.Fail();

			return Task.CompletedTask;
		}
	}
}
