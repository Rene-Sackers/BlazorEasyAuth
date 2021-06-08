using System.Threading.Tasks;
using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Services.Interfaces
{
	public interface IPolicyAuthorizationService
	{
		Task AuthorizeAsync(Policy policy, object resource);
		
		Task AuthorizeAsync(Policy policy);
	}
}