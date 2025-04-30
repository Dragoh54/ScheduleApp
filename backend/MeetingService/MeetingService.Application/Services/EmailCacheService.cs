using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class EmailCacheService(
    IDistributedCache cache,
    IEmailTokenProvider emailTokenProvider
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
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(emailTokenProvider.GetTokenExistingTime(type))
        }, cancellationToken);
    }
}