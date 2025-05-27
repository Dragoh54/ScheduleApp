using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class EmailCacheService: CacheService<string>, IEmailCacheService
{
    public EmailCacheService(IDistributedCache cache) : base(cache)
    {
    }
    
    public async Task AddEmailTokenToCacheAsync(string key, string token, 
        TokenTypes type, CancellationToken cancellationToken)
    {
        await SetStringAsync(token, key, cancellationToken);
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