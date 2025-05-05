using System.Text;
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
    
    public async Task AddEmailTokenToCacheAsync(string key, string token, TokenTypes type, CancellationToken cancellationToken)
    {
        await _cache.SetStringAsync(key, token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(emailTokenProvider.GetTokenExistingTime(type))
        }, cancellationToken);
    }

    public string CreateParticipantEmailTokenKey(Guid meetingId, string email)
    {
        var sb = new StringBuilder();
        
        sb.Append(meetingId);
        sb.Append('_');
        sb.Append(email);
        sb.Append('_');
        sb.Append("Token");
        
        return sb.ToString();
    }
}