using System.Security.Claims;
using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using Microsoft.AspNetCore.Http;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IPolicyAuthorizationService
	{
		Task<bool> AuthorizeAsync(Policy policy, bool throwIfUnauthorized = true);
		Task<bool> AuthorizeAsync(HttpContext httpContext, Policy policy, bool throwIfUnauthorized = true);
		Task<bool> AuthorizeAsync(ClaimsPrincipal claimsPrincipal, Policy policy, bool throwIfUnauthorized = true);
		
		Task<bool> AuthorizeAsync(Policy policy, object resource, bool throwIfUnauthorized = true);
		Task<bool> AuthorizeAsync(HttpContext httpContext, Policy policy, object resource, bool throwIfUnauthorized = true);
		Task<bool> AuthorizeAsync(ClaimsPrincipal claimsPrincipal, Policy policy, object resource, bool throwIfUnauthorized = true);
	}
}