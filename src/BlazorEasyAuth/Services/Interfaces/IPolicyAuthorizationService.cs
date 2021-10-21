using System.Threading.Tasks;
using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IPolicyAuthorizationService
	{
		Task<bool> AuthorizeAsync(Policy policy, object resource, bool throwIfUnauthorized = true);
		
		Task<bool> AuthorizeAsync(Policy policy, bool throwIfUnauthorized = true);
	}
}