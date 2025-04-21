using Microsoft.Extensions.Caching.Distributed;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;

namespace UserService.Application.Services;

public class EmailCacheService(
    IDistributedCache cache, 
    IJwtProvider jwtProvider
    ) : CacheService<string>(cache), IEmailCacheService
{
    private readonly IDistributedCache _cache = cache;

    public async Task AddEmailTokenToCacheAsync(string email, string token, TokenTypes type, CancellationToken cancellationToken)
    {
        var tokenFromCache = await _cache.GetStringAsync(email, cancellationToken);
        
        if (tokenFromCache is not null)
        {
            throw new BadRequestException("Email Token already exists");
        }
        
        await _cache.SetStringAsync(email, token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(jwtProvider.GetTokenExistingTime(type))
        }, cancellationToken);
    }
}