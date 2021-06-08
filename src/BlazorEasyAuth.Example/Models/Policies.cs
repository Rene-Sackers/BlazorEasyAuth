using BlazorEasyAuth.Extensions;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;

namespace BlazorEasyAuth.Example.Models
{
	public static class Policies
	{
		public static class Users
		{
			public static readonly Policy View = new(p => p.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser));
			
			public static readonly Policy Edit = new(p => p
				.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser)
				.AddRelativeRoleRequirement(RoleRequirement.Lesser));
			
			public static readonly Policy Delete = new(p => p
				.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser)
				.AddRelativeRoleRequirement(RoleRequirement.Lesser));
		}

		public static class MyResource
		{
			public static readonly Policy View = new(p => p.RequireRole(Roles.MyRole, Roles.Administrator, Roles.Superuser));
			public static readonly Policy Edit = new(p => p.RequireRole(Roles.MyRole, Roles.Administrator, Roles.Superuser));
			public static readonly Policy Create = new(p => p.RequireRole(Roles.MyRole, Roles.Administrator, Roles.Superuser));
		}
	}
}