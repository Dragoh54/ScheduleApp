using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Interfaces.Services;

public interface IEmailCacheService : ICacheService<string>
{
    public Task AddEmailTokenToCacheAsync(string key, string token, TokenTypes type, CancellationToken cancellationToken);
    
    public string CreateParticipantEmailTokenKey(Guid meetingId, string email);
}