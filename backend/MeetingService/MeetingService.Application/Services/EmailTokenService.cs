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
        if (token is not null)
        {
            return token;
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
        
        var tokenFromCache = await emailCacheService.GetStringAsync(key, cancellationToken)
            ?? throw new NotFoundException("Token is not found or expired");
        
        var success = string.Equals(tokenFromCache, token);
        
        await emailCacheService.DeleteAsync(key, cancellationToken);
        
        return success;
    }

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken) =>
        await jwtProvider.GetClaimFromToken(token, ClaimTypes.Email);
}