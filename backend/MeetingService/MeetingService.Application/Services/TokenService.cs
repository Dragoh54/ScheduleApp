using System.Security.Claims;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Enums;
using Microsoft.AspNetCore.WebUtilities;

namespace MeetingService.Application.Services;

public class TokenService(
    IJwtProvider jwtProvider, 
    IEmailTokenProvider emailTokenProvider,
    IEmailCacheService emailCacheService
    ) : ITokenService
{
    public async Task<string> GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var key = emailCacheService.CreateParticipantEmailTokenKey(meetingId, email);
        
        var token = await emailCacheService.Get(key, cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }
        
        var confirmToken = emailTokenProvider.GenerateEmailToken(meetingId, email, tokenType, cancellationToken)
                           ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        await emailCacheService.AddEmailTokenToCacheAsync(key, confirmToken, tokenType, cancellationToken);
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        return confirmToken;
    }

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken) =>
        await jwtProvider.GetClaimFromToken(token, ClaimTypes.Email);
}