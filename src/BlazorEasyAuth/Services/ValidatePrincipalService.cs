using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using BlazorEasyAuth.Extensions;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Providers.Interfaces;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorEasyAuth.Services
{
	internal class ValidatePrincipalService : IValidatePrincipalService
	{
		private static readonly ConcurrentDictionary<string, DateTimeOffset> PermissionChangeDates = new();
		
		private readonly IUserProvider _userProvider;

		public ValidatePrincipalService(IUserProvider userProvider)
		{
			_userProvider = userProvider;
		}
		
		public void NotifyPermissionsChanged(string userId)
		{
			PermissionChangeDates.TryRemove(userId, out _);
		}
		
		public void NotifyPermissionsChanged(IUser user)
		{
			NotifyPermissionsChanged(user.GetId());
		}

		private async Task<DateTimeOffset?> GetPermissionsChangedDateForUser(string userId)
		{
			if (PermissionChangeDates.TryGetValue(userId, out var knownDate))
				return knownDate;
			
			var date = (await _userProvider.GetById(userId))?.PermissionsChangeDate;
			if (date.HasValue)
				PermissionChangeDates.TryAdd(userId, date.Value);

			return date;
		}
		
		public async Task ValidatePrincipal(CookieValidatePrincipalContext context)
		{
			if (context.Principal?.Identity?.IsAuthenticated != true)
				return;

			var lastChangeDate = await GetPermissionsChangedDateForUser(context.Principal.GetId());
			if (!context.Properties.IssuedUtc.HasValue || context.Properties.IssuedUtc <= lastChangeDate)
			{
				context.RejectPrincipal();
				await context.HttpContext.SignOutAsync();
				// TODO: Find some fucking way to instantly sign someone out
				// NavigationManager is not initialized at this point, so we can't navigate away
			}
		}
	}
}