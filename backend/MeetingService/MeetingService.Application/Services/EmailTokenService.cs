using System.Security.Claims;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class EmailTokenService : IEmailTokenService
{
    public EmailTokenService(IJwtProvider jwtProvider, IDistributedCache cache,
        IEmailTokenProvider emailTokenProvider, IEmailCacheService emailCacheService
    )
    {
        _jwtProvider = jwtProvider;
        _cache = cache;
        _emailTokenProvider = emailTokenProvider;
        _emailCacheService = emailCacheService;
    }

    private readonly IJwtProvider _jwtProvider;
    private readonly IDistributedCache _cache;
    private readonly IEmailTokenProvider _emailTokenProvider;
    private readonly IEmailCacheService _emailCacheService;
    
    public async Task<string> GenerateEmailToken(Guid meetingId, string email, 
        TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var key = _emailCacheService.CreateParticipantEmailTokenKey(meetingId, email);
        
        var token = await _cache.GetStringAsync(key, cancellationToken);
        if (token is not null)
        {
            return token;
        }
        
        var confirmToken = _emailTokenProvider.GenerateEmailToken(meetingId, email, tokenType, cancellationToken)
                           ?? throw new UnauthorizedAccessException("Failed to generate token.");
        
        confirmToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(confirmToken));
        
        await _emailCacheService.AddEmailTokenToCacheAsync(key, confirmToken, tokenType, cancellationToken);
        
        return confirmToken;
    }

    public async Task<bool> CheckEmailToken(Guid meetingId, string email, string token, 
        CancellationToken cancellationToken)
    { 
        var key = _emailCacheService.CreateParticipantEmailTokenKey(meetingId, email);
        
        var tokenFromCache = await _emailCacheService.GetStringAsync(key, cancellationToken)
            ?? throw new NotFoundException("Token is not found or expired");
        
        var success = string.Equals(tokenFromCache, token);
        
        await _emailCacheService.DeleteAsync(key, cancellationToken);
        
        return success;
    }

    public async Task<string> GetEmailFromToken(string token, CancellationToken cancellationToken) =>
        await _jwtProvider.GetClaimFromToken(token, ClaimTypes.Email);
}