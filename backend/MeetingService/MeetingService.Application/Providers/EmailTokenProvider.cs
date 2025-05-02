using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Settings;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetingService.Application.Providers;

public class EmailTokenProvider(
    IOptions<JwtSettings> jwtSettings,
    IEmailCacheService emailCacheService
    ) : IEmailTokenProvider
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public async Task<string> GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var tokenKey = emailCacheService.CreateParticipantEmailTokenKey(meetingId, email);
        
        var token = await emailCacheService.Get(tokenKey, cancellationToken);
        if (!string.IsNullOrEmpty(token))
        {
            return token;
        }

        var confirmToken = Encoding.UTF8.GetBytes(email);
        var newToken = WebEncoders.Base64UrlEncode(confirmToken);
        
        await emailCacheService.AddEmailTokenToCacheAsync(tokenKey, newToken, tokenType, GetTokenExistingTime(tokenType), cancellationToken);
        
        return newToken;
    }

    public int GetTokenExistingTime(TokenTypes tokenTypesType)
    {
        return tokenTypesType switch
        {
            TokenTypes.ParticipantConfirmation => _jwtSettings.ParticipantConfirmationExpiresHours,
            TokenTypes.ParticipantDeclination => _jwtSettings.ParticipantDeclinationExpiresHours,
            _ => throw new BadRequestException("Invalid token type")
        };
    }


    public bool ValidateEmailToken(byte[] stringBytes, string email)
    {
        var decodedString = Encoding.UTF8.GetString(stringBytes);
        return email.Equals(decodedString);
    }
}