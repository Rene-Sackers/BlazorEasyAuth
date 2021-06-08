using BlazorEasyAuth.Models;

namespace BlazorEasyAuth.Example.Models
{
	public static class Roles
	{
		public static readonly Role Superuser = new(1000);
		public static readonly Role Administrator = new(999);

		public static readonly Role ManageUsers = new();
		public static readonly Role MyRole = new();
	}
}