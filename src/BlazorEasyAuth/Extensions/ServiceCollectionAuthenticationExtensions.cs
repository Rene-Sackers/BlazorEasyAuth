using System;
using System.Linq;
using System.Threading.Tasks;
using BlazorEasyAuth.Handlers;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Providers.Interfaces;
using BlazorEasyAuth.Services;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;

namespace BlazorEasyAuth.Extensions
{
    public static class ServiceCollectionAuthenticationExtensions
    {
        /// <summary>
        /// Make sure all policies are instantiated before calling this method.
        /// </summary>
        public static IServiceCollection AddBlazorEasyAuth<TUserProvider>(this IServiceCollection services, Action<AuthorizationOptions> configure)
            where TUserProvider : class, IUserProvider
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            var authBuilder = services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            });

            authBuilder
                .Services
                .TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<CookieAuthenticationOptions>, PostConfigureCookieAuthenticationOptions>());

            authBuilder
                .Services
                .AddOptions<CookieAuthenticationOptions>(CookieAuthenticationDefaults.AuthenticationScheme)
                .Validate(o => o.Cookie.Expiration == null, "Cookie.Expiration is ignored, use ExpireTimeSpan instead.");

            authBuilder
                .AddScheme<CookieAuthenticationOptions, CookieAuthenticationHandler>(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    null,
                    options =>
                    {
                        options.ExpireTimeSpan = TimeSpan.FromDays(7);
                        options.Cookie.SameSite = SameSiteMode.Lax;
                        options.SlidingExpiration = true;
                        options.LoginPath = Urls.SignInPageUrl;
                        options.LogoutPath = Urls.SignOutUrl;
                        options.Events.OnValidatePrincipal = context => context.HttpContext.RequestServices.GetRequiredService<IValidatePrincipalService>().ValidatePrincipal(context);
                    });

            services
                .AddAuthorizationCore(configure)
                .AddAuthorizationPolicyEvaluator();

            services
                .AddSingleton<IAuthorizationHandler, RelativeRoleRequirementHandler>()
                .AddSingleton<IAuthorizationHandler, RelativeRoleByUserRequirementHandler>()
                .AddSingleton<ISignInTokenService, SignInTokenService>()
                .AddScoped<IValidatePrincipalService, ValidatePrincipalService>()
                .AddScoped<IPolicyAuthorizationService, PolicyAuthorizationService>()
                .AddScoped<IUserAuthenticationService, UserAuthenticationService>()
                .AddTransient<IRoleRequirementTestService, RoleRequirementTestService>()
                .AddTransient<IUserProvider, TUserProvider>();

            return services;
        }
    }
}