using BlazorEasyAuth.Example.Models;
using BlazorEasyAuth.Example.Providers;
using BlazorEasyAuth.Example.Providers.Interfaces;
using BlazorEasyAuth.Extensions;
using BlazorEasyAuth.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BlazorEasyAuth.Example
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddRazorPages();
			services.AddServerSideBlazor();

			services.AddSingleton<IDatabaseSampleUsersProvider, DatabaseSampleDatabaseUsersProvider>();
			
			services.AddBlazorEasyAuth<UserProvider>(ConfigurePolicies);
		}

		private static void ConfigurePolicies(AuthorizationOptions authorizationOptions)
		{
			// Users
			authorizationOptions
				.AddPolicy(Policies.Users.View, p => p
					.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser));
			
			authorizationOptions
				.AddPolicy(Policies.Users.Edit, p => p
					.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser)
					.AddRelativeRoleRequirement(RoleRequirement.Lesser));
			
			authorizationOptions
				.AddPolicy(Policies.Users.Delete, p => p
					.RequireRole(Roles.ManageUsers, Roles.Administrator, Roles.Superuser)
					.AddRelativeRoleRequirement(RoleRequirement.Lesser));
			
			// MyResource1
			authorizationOptions
				.AddPolicy(Policies.MyResource1.View, p => p.RequireRole(Roles.MyRole1, Roles.Administrator, Roles.Superuser));
			authorizationOptions
				.AddPolicy(Policies.MyResource1.Edit, p => p.RequireRole(Roles.Administrator, Roles.Superuser));
			authorizationOptions
				.AddPolicy(Policies.MyResource1.Create, p => p.RequireRole(Roles.Superuser));
			authorizationOptions
				.AddPolicy(Policies.MyResource1.Delete, p => p.RequireRole(Roles.MyRole1));
			
			// MyResource2
			authorizationOptions
				.AddPolicy(Policies.MyResource2.View, p => p.RequireRole(Roles.MyRole2, Roles.Administrator, Roles.Superuser));
			authorizationOptions
				.AddPolicy(Policies.MyResource2.Edit, p => p.RequireRole(Roles.Administrator, Roles.Superuser));
			authorizationOptions
				.AddPolicy(Policies.MyResource2.Create, p => p.RequireRole(Roles.Superuser));
			authorizationOptions
				.AddPolicy(Policies.MyResource2.Delete, p => p.RequireRole(Roles.MyRole2));
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseCookiePolicy();
			app.UseAuthentication();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
				endpoints.MapBlazorHub();
				endpoints.MapFallbackToPage("/_Host");
			});
		}
	}
}