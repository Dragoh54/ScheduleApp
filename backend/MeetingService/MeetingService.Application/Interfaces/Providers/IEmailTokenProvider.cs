using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Providers;

public interface IEmailTokenProvider
{
    public Task<string> GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken);
    public int GetTokenExistingTime(TokenTypes tokenTypesType);
    public bool ValidateEmailToken(byte[] stringBytes, string email);
}