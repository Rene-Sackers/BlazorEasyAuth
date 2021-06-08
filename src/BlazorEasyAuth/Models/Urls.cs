using System;

namespace BlazorEasyAuth.Models
{
    public static class Urls
    {
        public const string SignInPageUrl = "/authentication/signin";
        public const string SignInActionUrl = "/authentication/signinaction";
        public const string SignOutUrl = "/authentication/signout";

        public static string GetSignInPageUrl(string returnUrl = null)
        {
            returnUrl = string.IsNullOrWhiteSpace(returnUrl)
                ? null
                : $"/{Uri.EscapeDataString(returnUrl)}";
            
            return $"{SignInPageUrl}{returnUrl}";
        }

        public static string GetSignInActionUrl(string token, string returnUrl = null)
        {
            token = $"?token={Uri.EscapeDataString(token)}";
            returnUrl = string.IsNullOrWhiteSpace(returnUrl)
                ? null
                : $"&returnUrl={Uri.EscapeDataString(returnUrl)}";
            
            return $"{SignInActionUrl}{token}{returnUrl}";
        }
    }
}