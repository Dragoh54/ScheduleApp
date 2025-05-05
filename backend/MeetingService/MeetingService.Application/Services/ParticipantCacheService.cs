using System.Text;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class ParticipantCacheService(
    IDistributedCache cache
    ) : CacheService<Participant>(cache), IParticipantCacheService
{
    public string CreateKey(Guid meetingId, string email)
    {
        var sb = new StringBuilder();
        
        sb.Append(meetingId);
        sb.Append('_');
        sb.Append(email);
        sb.Append('_');
        sb.Append("Participant");
        
        return sb.ToString();
    }
}