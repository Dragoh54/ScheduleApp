using Microsoft.Extensions.Caching.Distributed;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;

namespace UserService.Application.Services;

public class CacheService(
    IDistributedCache cache, 
    IJwtProvider jwtProvider
    ) : ICacheService
{
    public async Task AddEmailTokenToCacheAsync(string email,string token, TokenTypes type, CancellationToken cancellationToken)
    {
        var tokenFromCache = await cache.GetStringAsync(email, cancellationToken);
        if (tokenFromCache is not null)
        {
            throw new BadRequestException("Email Token already exists");
        }
        
        await cache.SetStringAsync(email, token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(jwtProvider.GetTokenExistingTime(type))
        }, cancellationToken);
    }
}