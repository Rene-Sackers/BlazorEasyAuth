using System;
using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEasyAuth.Services
{
	internal class PolicyAuthorizationService : IPolicyAuthorizationService
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly IUserAuthenticationService _userAuthenticationService;

		public PolicyAuthorizationService(
			IAuthorizationService authorizationService,
			IUserAuthenticationService userAuthenticationService)
		{
			_authorizationService = authorizationService;
			_userAuthenticationService = userAuthenticationService;
		}

		public async Task AuthorizeAsync(Policy policy, object resource)
		{
			var claimsPrincipal = await _userAuthenticationService.GetCachedClaimsIdentityAsync();
			var result = await _authorizationService.AuthorizeAsync(claimsPrincipal, resource, policy.Name);

			if (!result.Succeeded)
				throw new UnauthorizedAccessException($"User {claimsPrincipal.Identity?.Name} is not allowed to execute policy {policy.Name}");
		}

		public async Task AuthorizeAsync(Policy policy)
		{
			var claimsPrincipal = await _userAuthenticationService.GetCachedClaimsIdentityAsync();
			var result = await _authorizationService.AuthorizeAsync(claimsPrincipal, policy.Name);

			if (!result.Succeeded)
				throw new UnauthorizedAccessException($"User {claimsPrincipal.Identity?.Name} is not allowed to execute policy {policy.Name}");
		}
	}
}
