using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Providers;

public interface IEmailTokenProvider
{
    public string GenerateEmailToken(string email);
    public int GetTokenExistingTime(TokenTypes tokenTypesType);
    public bool ValidateEmailToken(byte[] stringBytes, string email);
}