using BlazorEasyAuth.Extensions;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Requirements;

namespace BlazorEasyAuth.Example.Models
{
	public static class Policies
	{
		public static class Users
		{
			public static readonly Policy View = new();
			public static readonly Policy Edit = new();
			public static readonly Policy Delete = new();
		}

		public static class MyResource1
		{
			public static readonly Policy View = new();
			public static readonly Policy Edit = new();
			public static readonly Policy Create = new();
			public static readonly Policy Delete = new();
		}

		public static class MyResource2
		{
			public static readonly Policy View = new();
			public static readonly Policy Edit = new();
			public static readonly Policy Create = new();
			public static readonly Policy Delete = new();
		}
	}
}