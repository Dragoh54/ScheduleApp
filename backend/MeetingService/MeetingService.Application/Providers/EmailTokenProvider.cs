using System.Security.Claims;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Settings;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MeetingService.Application.Providers;

public class EmailTokenProvider(
    IConfiguration configuration, 
    IOptions<JwtSettings> jwtSettings
    ) : IEmailTokenProvider
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;
    
    // public string GenerateEmailToken(Participant participant, TokenTypes tokenTypesType, CancellationToken cancellationToken)
    // {
        // List<Claim> claims=
        // [
        //     new(ClaimTypes.Email, participant.Email),
        // ];
        //
        // var signingCredentials = new SigningCredentials(
        //     new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
        //     SecurityAlgorithms.HmacSha256);
        //
        // var token = new JwtSecurityToken(
        //     claims: claims,
        //     signingCredentials: signingCredentials,
        //     expires: GetExpirationDate(tokenTypes)
        // );
        //  
        // var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        // cancellationToken.ThrowIfCancellationRequested();
        //  
        // return tokenValue;
    // }

    public string GenerateEmailToken(string email)
    {
        var stringBytes = Encoding.ASCII.GetBytes(email);
        return Convert.ToBase64String(stringBytes);
    }

    public int GetTokenExistingTime(TokenTypes tokenTypesType)
    {
        return tokenTypesType switch
        {
            TokenTypes.ParticipantConfirmation => _jwtSettings.ParticipantConfirmationExpiresHours,
            TokenTypes.ParticipantStatusChanged => _jwtSettings.ParticipantStatusChangedExpiresHours,
            _ => throw new BadRequestException("Invalid token type")
        };
    }

    public bool ValidateEmailToken(byte[] stringBytes, string email)
    {
        var decodedString = Encoding.UTF8.GetString(stringBytes);
        return email.Equals(decodedString);
    }
}