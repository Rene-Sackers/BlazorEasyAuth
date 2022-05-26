#nullable enable
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using BlazorEasyAuth.Models;
using BlazorEasyAuth.Providers.Interfaces;
using BlazorEasyAuth.Services.Interfaces;

namespace BlazorEasyAuth.Services
{
	internal class SignInTokenService : ISignInTokenService
	{
		private readonly IUserProvider _userProvider;
		private readonly ConcurrentDictionary<string, SignInToken> _signInTokens = new();

		private const int TokenExpirationInSeconds = 60;
		
		public SignInTokenService(IUserProvider userProvider)
		{
			_userProvider = userProvider;
		}

		public Task<string> CreateTokenAsync(string userId)
		{
			_signInTokens
				.Where(sit => sit.Value.UserId == userId)
				.Select(kv => kv.Key)
				.ToList()
				.ForEach(key => _signInTokens.TryRemove(key, out _));

			var token = new SignInToken
			{
				UserId = userId,
				Token = Guid.NewGuid().ToString(),
				ExpirationDate = DateTimeOffset.Now.AddSeconds(TokenExpirationInSeconds)
			};

			return !_signInTokens.TryAdd(token.Token, token)
				? CreateTokenAsync(userId)
				: Task.FromResult(token.Token);
		}

		public Task<string?> GetUserIdForTokenAsync(string token)
		{
			if (!_signInTokens.TryGetValue(token, out var existingToken))
				return Task.FromResult<string?>(null);

			_signInTokens.TryRemove(token, out _);

			return Task.FromResult(existingToken.ExpirationDate < DateTimeOffset.Now
				? null
				: existingToken.UserId);
		}

		public async Task<IUser?> GetUserForSignInTokenAsync(string token)
		{
			var userId = await GetUserIdForTokenAsync(token);

			if (userId == null)
				return null;

			var user = await _userProvider.GetById(userId);

			return user.IsDeleted ? null : user;
		}
	}
}
