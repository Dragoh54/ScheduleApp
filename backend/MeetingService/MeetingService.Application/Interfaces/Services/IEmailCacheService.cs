using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Interfaces.Services;

//TODO: IMPLEMENT THIS
public interface IEmailCacheService : ICacheService<string>
{
    public Task AddEmailTokenToCacheAsync(string email, string token, TokenTypes type, CancellationToken cancellationToken);
}