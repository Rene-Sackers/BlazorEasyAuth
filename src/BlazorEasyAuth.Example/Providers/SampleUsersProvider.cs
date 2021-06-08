using System;
using System.Collections.Generic;
using System.Linq;
using BlazorEasyAuth.Example.Models;
using BlazorEasyAuth.Example.Providers.Interfaces;
using BlazorEasyAuth.Helpers;

namespace BlazorEasyAuth.Example.Providers
{
	public class SampleUsersProvider : ISampleUsersProvider
	{
		private readonly List<User> _allUsers;

		public SampleUsersProvider()
		{
			_allUsers = new()
			{
				new()
				{
					Id = 1,
					Username = "user1",
					DisplayName = "User 1",
					PasswordHash = HashHelper.GetPasswordHash("user1password"),
					Roles = new [] { "Administrator" }
				}
			};
		}
		
		public User GetById(int id)
			=> _allUsers.FirstOrDefault(u => u.Id == id);

		public User GetByCredentials(string username, string password)
		{
			var passwordHash = HashHelper.GetPasswordHash(password);
			return _allUsers.FirstOrDefault(u => string.Equals(u.Username, username, StringComparison.InvariantCulture) && u.PasswordHash.SequenceEqual(passwordHash));
		}
	}
}