using System.Threading.Tasks;
using BlazorEasyAuth.Example.Providers.Interfaces;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Providers.Interfaces;

namespace BlazorEasyAuth.Example.Providers
{
	public class UserProvider : IUserProvider
	{
		private readonly ISampleUsersProvider _sampleUsersProvider;

		public UserProvider(ISampleUsersProvider sampleUsersProvider)
		{
			_sampleUsersProvider = sampleUsersProvider;
		}
		
		public Task<IUser> GetById(string id)
			=> Task.FromResult((IUser) _sampleUsersProvider.GetById(int.Parse(id)));
	}
}