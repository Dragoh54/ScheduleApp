using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Services;

public interface IParticipantCacheService  : ICacheService<Participant>
{
    public Task AddParticipantToCacheAsync(Participant participant, CancellationToken cancellationToken);
    public Task<Participant> GetParticipantFromCache(Guid meetingId, string email, CancellationToken cancellationToken);
}