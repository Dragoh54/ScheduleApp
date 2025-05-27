using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Settings;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetingService.Application.Providers;

public class EmailTokenProvider : JwtProvider, IEmailTokenProvider
{
    public EmailTokenProvider(IConfiguration configuration, IOptions<JwtSettings> jwtSettings)
    : base(configuration, jwtSettings)
    {
    }
    
    public string GenerateEmailToken(Guid meetingId, string email, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        List<Claim> claims=
        [
            new(ClaimTypes.Email, email)
        ];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: GetExpirationDate(tokenType)
        );
         
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        cancellationToken.ThrowIfCancellationRequested();
         
        return tokenValue;
    }

    public int GetTokenExistingTime(TokenTypes tokenTypesType)
    {
        return tokenTypesType switch
        {
            TokenTypes.ParticipantConfirmation => JwtSettings.ParticipantConfirmationExpiresHours,
            TokenTypes.ParticipantDeclination => JwtSettings.ParticipantDeclinationExpiresHours,
            _ => throw new BadRequestException("Invalid token type")
        };
    }

    public DateTime GetExpirationDate(TokenTypes tokenType)
    {
        return tokenType switch
        {
            TokenTypes.ParticipantConfirmation => DateTime.UtcNow.AddHours(
                JwtSettings.ParticipantConfirmationExpiresHours),
            TokenTypes.ParticipantDeclination => DateTime.UtcNow.AddHours(
                JwtSettings.ParticipantDeclinationExpiresHours),
            _ => throw new BadRequestException("Invalid token type")
        };
    }
}