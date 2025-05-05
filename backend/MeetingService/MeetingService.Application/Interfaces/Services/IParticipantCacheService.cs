using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Services;

public interface IParticipantCacheService  : ICacheService<Participant>
{
    public string CreateKey(Guid meetingId, string email);
}