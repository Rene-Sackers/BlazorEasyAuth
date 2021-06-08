using System.Threading.Tasks;
using BlazorEasyAuth.Models;

#nullable enable
namespace BlazorEasyAuth.Services.Interfaces
{
	public interface ISignInTokenService
	{
		string CreateToken(string userId);
		
		string? GetUserIdForToken(string token);
		Task<IUser?> GetUserForSignInTokenAsync(string token);
	}
}