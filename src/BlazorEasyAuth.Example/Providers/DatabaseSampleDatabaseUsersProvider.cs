using System;
using System.Collections.Generic;
using System.Linq;
using BlazorEasyAuth.Example.Models;
using BlazorEasyAuth.Example.Providers.Interfaces;
using BlazorEasyAuth.Helpers;

namespace BlazorEasyAuth.Example.Providers
{
	public class DatabaseSampleDatabaseUsersProvider : IDatabaseSampleUsersProvider
	{
		private readonly List<User> _allUsers;

		public DatabaseSampleDatabaseUsersProvider()
		{
			_allUsers = new()
			{
				new()
				{
					Id = 1,
					Username = "superuser",
					DisplayName = "Superuser",
					PasswordHash = HashHelper.GetPasswordHash("superuser"),
					Roles = new string[] { Roles.Superuser }
				},
				new()
				{
					Id = 2,
					Username = "admin",
					DisplayName = "Administrator",
					PasswordHash = HashHelper.GetPasswordHash("admin"),
					Roles = new string[] { Roles.Administrator }
				},
				new()
				{
					Id = 3,
					Username = "myrole1",
					DisplayName = "My Role 1",
					PasswordHash = HashHelper.GetPasswordHash("myrole1"),
					Roles = new string[] { Roles.MyRole1 }
				},
				new()
				{
					Id = 4,
					Username = "myrole2",
					DisplayName = "My Role 2",
					PasswordHash = HashHelper.GetPasswordHash("myrole2"),
					Roles = new string[] { Roles.MyRole2 }
				},
				new()
				{
					Id = 5,
					Username = "myrole1and2",
					DisplayName = "My Role 1 and 2",
					PasswordHash = HashHelper.GetPasswordHash("myrole1and2"),
					Roles = new string[] { Roles.MyRole1, Roles.MyRole2 }
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