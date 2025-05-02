using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Interfaces.Services;

public interface ITokenService
{
    public Task<string> GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken);
    
    Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken);
}