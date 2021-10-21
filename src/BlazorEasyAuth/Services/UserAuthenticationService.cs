using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorEasyAuth.Services
{
	internal class UserAuthenticationService : IUserAuthenticationService
	{
		private readonly AuthenticationStateProvider _authenticationStateProvider;

		public UserAuthenticationService(AuthenticationStateProvider authenticationStateProvider)
		{
			_authenticationStateProvider = authenticationStateProvider;
		}

		public ClaimsIdentity CreateClaimsIdentity(IUser user)
		{
			var claims = new List<Claim>
			{
				new (ClaimTypes.NameIdentifier, user.Username),
				new (ClaimTypes.Name, user.DisplayName),
				new (ClaimTypes.Sid, user.GetId())
			};

			claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)));

			return new(claims, CookieAuthenticationDefaults.AuthenticationScheme);
		}

		public async Task<bool> IsAuthenticatedAsync()
		{
			return (await _authenticationStateProvider.GetAuthenticationStateAsync())?.User.Identity?.IsAuthenticated == true;
		}
	}
}
