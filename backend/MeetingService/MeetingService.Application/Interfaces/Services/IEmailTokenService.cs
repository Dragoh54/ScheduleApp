using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Interfaces.Services;

public interface IEmailTokenService
{
    public Task<string> GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken);
    
    public Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken);
    public Task<string> GetParticipantStatusFromToken(string token, CancellationToken cancellationToken);
    
    public Task<bool> CheckEmailToken(Guid meetingId, string email, string token, CancellationToken cancellationToken);
}