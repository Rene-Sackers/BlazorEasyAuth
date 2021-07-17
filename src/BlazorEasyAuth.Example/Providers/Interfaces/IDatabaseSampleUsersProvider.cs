using BlazorEasyAuth.Example.Models;

namespace BlazorEasyAuth.Example.Providers.Interfaces
{
	public interface IDatabaseSampleUsersProvider
	{
		User GetById(int id);
		
		User GetByCredentials(string username, string password);
	}
}