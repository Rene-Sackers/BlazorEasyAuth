using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IValidatePrincipalService
	{
		public Task ValidatePrincipal(CookieValidatePrincipalContext context);
		
		void NotifyPermissionsChanged(IUser user);
		
		void NotifyPermissionsChanged(string userId);
	}
}