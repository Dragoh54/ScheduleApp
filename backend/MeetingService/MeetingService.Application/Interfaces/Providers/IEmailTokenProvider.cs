using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Providers;

public interface IEmailTokenProvider : IJwtProvider
{
    public string GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken);
    public int GetTokenExistingTime(TokenTypes tokenTypesType);
    DateTime GetExpirationDate(TokenTypes tokenType);
}