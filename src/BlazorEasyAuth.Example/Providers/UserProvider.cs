using System.Threading.Tasks;
using BlazorEasyAuth.Example.Providers.Interfaces;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Providers.Interfaces;

namespace BlazorEasyAuth.Example.Providers
{
	public class UserProvider : IUserProvider
	{
		private readonly IDatabaseSampleUsersProvider _databaseSampleUsersProvider;

		public UserProvider(IDatabaseSampleUsersProvider databaseSampleUsersProvider)
		{
			_databaseSampleUsersProvider = databaseSampleUsersProvider;
		}
		
		public Task<IUser> GetById(string id)
			=> Task.FromResult((IUser) _databaseSampleUsersProvider.GetById(int.Parse(id)));
	}
}