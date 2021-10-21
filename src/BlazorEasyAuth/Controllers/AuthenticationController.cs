using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace BlazorEasyAuth.Controllers
{
	public class AuthenticationController : Controller
	{
		private readonly IUserAuthenticationService _userAuthenticationService;
		private readonly ISignInTokenService _signInTokenService;

		public AuthenticationController(
			IUserAuthenticationService userAuthenticationService,
			ISignInTokenService signInTokenService)
		{
			_userAuthenticationService = userAuthenticationService;
			_signInTokenService = signInTokenService;
		}

		[Route(Urls.SignInActionUrl)]
		public async Task<IActionResult> SignInAction(string token, string returnUrl)
		{
			var user = await _signInTokenService.GetUserForSignInTokenAsync(token);

			if (user == null)
				return Redirect(Urls.SignInPageUrl);

			var authenticationProperties = new AuthenticationProperties
			{
				AllowRefresh = true,
				IsPersistent = true,
				RedirectUri = "/"
			};

			var claimsIdentity = _userAuthenticationService.CreateClaimsIdentity(user);

			await HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				new(claimsIdentity),
				authenticationProperties);

			return string.IsNullOrWhiteSpace(returnUrl)
				? Redirect("/")
				: Redirect("/" + returnUrl.TrimStart('/'));
		}

		[Route(Urls.SignOutUrl)]
		public async Task<IActionResult> SignOut()
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			return Redirect(Urls.SignInPageUrl);
		}
	}
}