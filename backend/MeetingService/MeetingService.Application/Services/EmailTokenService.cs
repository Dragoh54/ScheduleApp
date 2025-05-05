using System.Security.Claims;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class EmailTokenService(
    IJwtProvider jwtProvider, 
    IDistributedCache cache, 
    IEmailTokenProvider emailTokenProvider,
    IEmailCacheService emailCacheService
    ) : IEmailTokenService
{
    public async Task<string> GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var key = emailCacheService.CreateParticipantEmailTokenKey(meetingId, email);
        
        var token = await cache.GetStringAsync(key, cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            return WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
        }
        
        var confirmToken = emailTokenProvider.GenerateEmailToken(meetingId, email, tokenType, cancellationToken)
                           ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        
        await emailCacheService.AddEmailTokenToCacheAsync(key, confirmToken, tokenType, cancellationToken);
        
        return confirmToken;
    }

    public async Task<bool> CheckEmailToken(Guid meetingId, string email, string token, CancellationToken cancellationToken)
    { 
        var key = emailCacheService.CreateParticipantEmailTokenKey(meetingId, email);
        
        var tokenFromCache = await cache.GetStringAsync(key, cancellationToken)
            ?? throw new NotFoundException("Token is not found or expired");
        
        var success = CheckTokens(tokenFromCache, token);
        
        await emailCacheService.Delete(key, cancellationToken);
        
        return success;
    }

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken) =>
        await jwtProvider.GetClaimFromToken(token, ClaimTypes.Email);

    public async Task<string> GetParticipantStatusFromToken(string token, CancellationToken cancellationToken) =>
        await jwtProvider.GetClaimFromToken(token, "ParticipantStatus");

    private bool CheckTokens(string token, string encodedToken)
    {
        var decodedTokenFromDto = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(encodedToken));
        return token != decodedTokenFromDto;
    }
}