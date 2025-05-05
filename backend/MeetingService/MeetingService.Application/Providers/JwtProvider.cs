using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Settings;
using MeetingService.DomainModel.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MeetingService.Application.Providers;

public class JwtProvider(
    IConfiguration configuration, 
    IOptions<JwtSettings> jwtSettings
    ) : IJwtProvider
{
    private protected readonly JwtSettings JwtSettings = jwtSettings.Value;
    private protected readonly string SecretKey = configuration["JWTSecretKey"] ?? throw new NullReferenceException();
    
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey))
        };
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

        var isTokenInvalid = securityToken is not JwtSecurityToken jwtSecurityToken ||
                             !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                                 StringComparison.InvariantCultureIgnoreCase);
        
        if (isTokenInvalid)
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        return principal;
    }

    public async Task<string> GetClaimFromToken(string token, string claimType)
    {
        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException("Token cannot be empty", nameof(token));

        if (string.IsNullOrWhiteSpace(claimType))
            throw new ArgumentException("Claim type cannot be empty", nameof(claimType));

        var principal = ValidateToken(token)
                        ?? throw new BadRequestException("Invalid or expired token");

        var claim = principal.FindFirst(claimType)
                    ?? throw new BadRequestException($"Claim '{claimType}' not found in token");
        
        return claim.Value;
    }

    public async Task<Guid> GetUserIdFromToken(string token)
    {
        var id = await GetClaimFromToken(token, "Id");
        return Guid.Parse(id);
    }
}