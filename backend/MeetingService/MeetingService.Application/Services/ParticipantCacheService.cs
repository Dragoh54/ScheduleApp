using System.Text;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class ParticipantCacheService(
    IDistributedCache participantCache
    ) : CacheService<Participant>(participantCache), IParticipantCacheService
{
    public async Task AddParticipantToCacheAsync(Participant participant, CancellationToken cancellationToken)
    {
        var key = CreateKey(participant.MeetingId, participant.Email);
        var participantFromCache = await participantCache.GetStringAsync(key, cancellationToken);

        if (participantFromCache is not null)
        {
            await Delete(key, cancellationToken);
        }
        
        await Set(participant, key, cancellationToken);
    }

    public async Task<Participant> GetParticipantFromCache(Guid meetingId, string email, CancellationToken cancellationToken)
    {
        var key = CreateKey(meetingId, email);
        var participant = await Get(key, cancellationToken)
                          ?? throw new BadRequestException("Participant not found");

        await Delete(key, cancellationToken);
        
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