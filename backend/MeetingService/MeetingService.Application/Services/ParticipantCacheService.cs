using System.Text;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class ParticipantCacheService(
    IDistributedCache cache
    ) : CacheService<Participant>(cache), IParticipantCacheService
{
    public async Task AddParticipantToCacheAsync(Participant participant, CancellationToken cancellationToken)
    {
        var key = CreateKey(participant.MeetingId, participant.Email);
        var participantFromCache = await GetAsync(key, cancellationToken);

        if (participantFromCache is not null)
        {
            await DeleteAsync(key, cancellationToken);
        }
        
        await SetAsync(participant, key, cancellationToken);
    }

    public async Task<Participant> GetParticipantFromCache(Guid meetingId, string email, CancellationToken cancellationToken)
    {
        var key = CreateKey(meetingId, email);
        var participant = await GetAsync(key, cancellationToken)
                          ?? throw new BadRequestException("Participant not found");

        await DeleteAsync(key, cancellationToken);
        
        return participant;
    }
    
    private string CreateKey(Guid meetingId, string email)
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