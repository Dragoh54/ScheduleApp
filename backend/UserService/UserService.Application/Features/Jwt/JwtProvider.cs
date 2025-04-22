using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.Application.Interfaces.Auth;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Models;

namespace UserService.Application.Features.Jwt;

public class JwtProvider(
    IConfiguration configuration, 
    IOptions<JwtOptions> jwtOptions
    ) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    private readonly string _secretKey = configuration["JWTSecretKey"] ?? throw new NullReferenceException();
    
    public string GenerateAccessToken(UserEntity user, CancellationToken cancellationToken)
    {
        List<Claim> claims =
        [
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username)
        ];
        
        claims.AddRange(user.UserRoles.Select(
            role => new Claim(ClaimTypes.Role, role.Role.RoleName.ToString()))
        );

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.AccessExpiresMinutes)
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        cancellationToken.ThrowIfCancellationRequested();
        
        return tokenString;
    }

    public TokenEntity GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken)
    {
        var token =  new TokenEntity()
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(JwtConfig.Base64)),
            TokenType = TokenTypes.Refresh,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwtOptions.RefreshExpiresDays),
            IsUsed = false,
        };
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey))
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
    
    public string GenerateRefreshTokenString() => Convert.ToBase64String(RandomNumberGenerator.GetBytes(JwtConfig.Base64));
}