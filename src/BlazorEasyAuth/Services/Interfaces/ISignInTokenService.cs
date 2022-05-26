using System.Threading.Tasks;
using BlazorEasyAuth.Models;

#nullable enable
namespace BlazorEasyAuth.Services.Interfaces
{
	public interface ISignInTokenService
	{
		Task<string> CreateTokenAsync(string userId);
		
		Task<string?> GetUserIdForTokenAsync(string token);
		Task<IUser?> GetUserForSignInTokenAsync(string token);
	}
}