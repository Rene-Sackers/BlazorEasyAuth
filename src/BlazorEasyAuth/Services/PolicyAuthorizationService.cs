using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Http;

namespace BlazorEasyAuth.Services
{
	internal class PolicyAuthorizationService : IPolicyAuthorizationService
	{
		private readonly IAuthorizationService _authorizationService;
		private readonly NavigationManager _navigationManager;
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public PolicyAuthorizationService(
			IAuthorizationService authorizationService,
			NavigationManager navigationManager,
			AuthenticationStateProvider authenticationStateProvider)
		{
			_authorizationService = authorizationService;
			_navigationManager = navigationManager;
			_authenticationStateProvider = authenticationStateProvider;
		}

		public async Task<bool> AuthorizeAsync(Policy policy, object resource, bool throwIfUnauthorized = true)
		{
			var claimsPrincipal = (await _authenticationStateProvider.GetAuthenticationStateAsync())?.User;
			return await AuthorizeAsync(claimsPrincipal, policy, resource, throwIfUnauthorized);
		}

		public async Task<bool> AuthorizeAsync(HttpContext httpContext, Policy policy, object resource, bool throwIfUnauthorized = true)
			=> await AuthorizeAsync(httpContext.User, policy, resource, throwIfUnauthorized);

		public async Task<bool> AuthorizeAsync(ClaimsPrincipal claimsPrincipal, Policy policy, object resource, bool throwIfUnauthorized = true)
		{
			if (claimsPrincipal is not { Identity: { IsAuthenticated: true } })
			{
				if (throwIfUnauthorized)
					_navigationManager.NavigateTo(Urls.SignInPageUrl, true);
				return false;
			}
			
			var result = await _authorizationService.AuthorizeAsync(claimsPrincipal, resource, policy.Name);

			if (throwIfUnauthorized && !result.Succeeded)
				throw new UnauthorizedAccessException($"User {claimsPrincipal.Identity?.Name} is not allowed to execute policy {policy.Name}");

			return result.Succeeded;
		}

		public async Task<bool> AuthorizeAsync(Policy policy, bool throwIfUnauthorized = true)
		{
			var claimsPrincipal = (await _authenticationStateProvider.GetAuthenticationStateAsync())?.User;
			return await AuthorizeAsync(claimsPrincipal, policy, throwIfUnauthorized);
		}

		public async Task<bool> AuthorizeAsync(HttpContext httpContext, Policy policy, bool throwIfUnauthorized = true)
			=> await AuthorizeAsync(httpContext.User, policy, throwIfUnauthorized);

		public async Task<bool> AuthorizeAsync(ClaimsPrincipal claimsPrincipal, Policy policy, bool throwIfUnauthorized = true)
		{
			if (claimsPrincipal is not { Identity: { IsAuthenticated: true } })
			{
				if (throwIfUnauthorized)
					_navigationManager.NavigateTo(Urls.SignInPageUrl, true);
				return false;
			}
			
			var result = await _authorizationService.AuthorizeAsync(claimsPrincipal, policy.Name);

			if (throwIfUnauthorized && !result.Succeeded)
				throw new UnauthorizedAccessException($"User {claimsPrincipal.Identity?.Name} is not allowed to execute policy {policy.Name}");
			
			return result.Succeeded;
		}
	}
}
