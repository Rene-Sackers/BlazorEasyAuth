using System.Security.Claims;
using System.Threading.Tasks;
using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IUserAuthenticationService
	{
		ClaimsIdentity CreateClaimsIdentity(IUser user);
		
		Task<ClaimsPrincipal> GetCachedClaimsIdentityAsync();
	}
}
