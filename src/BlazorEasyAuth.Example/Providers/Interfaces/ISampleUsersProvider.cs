using BlazorEasyAuth.Example.Models;

namespace BlazorEasyAuth.Example.Providers.Interfaces
{
	public interface ISampleUsersProvider
	{
		User GetById(int id);
		
		User GetByCredentials(string username, string password);
	}
}