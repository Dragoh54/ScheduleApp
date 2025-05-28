using Microsoft.Extensions.Caching.Distributed;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Providers;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;

namespace UserService.Application.Services;

public class EmailCacheService : CacheService<string>, IEmailCacheService
{
    private readonly IEmailTokenProvider _emailTokenProvider;

    public EmailCacheService(IDistributedCache cache, IEmailTokenProvider emailTokenProvider) : base(cache)
    {
        _emailTokenProvider = emailTokenProvider;
    }

    public async Task AddEmailTokenToCacheAsync(string email, string token, TokenTypes type, CancellationToken cancellationToken)
    {
        var tokenFromCache = await GetAsync(email, cancellationToken);
        
        if (tokenFromCache is not null)
        {
            return;
        }

        var timeSpan = TimeSpan.FromHours(_emailTokenProvider.GetTokenExistingTime(type));
        await SetStringAsync(token, email, timeSpan, cancellationToken);
    }
}