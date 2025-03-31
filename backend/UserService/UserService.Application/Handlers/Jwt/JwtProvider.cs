using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using UserService.DataAccess.Interfaces.Auth;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Handlers.Jwt;

public class JwtProvider(IConfiguration configuration, IOptions<JwtOptions> jwtOptions) : IJwtProvider
{
    private readonly JwtOptions _jwtOptions = jwtOptions.Value;
    
    private readonly string _secretKey = configuration["JWTSecretKey"] ?? throw new NullReferenceException();
    
    public string GenerateToken(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken)
    {
        List<Claim> claims=
        [
            new("Id", user.Id.ToString()),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Username)
        ];

        foreach (var role in user.UserRoles)
        {
            claims.Add(new (ClaimTypes.Role, role.Role.RoleName.ToString()));
        }

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey)),
            SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: GetExpirationDate(tokenTypesType)
        );
        
        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        cancellationToken.ThrowIfCancellationRequested();
        
        return tokenValue;
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
        
        if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }
        
        return principal;
    }

    public DateTime GetExpirationDate(TokenTypes tokenTypesType)
    {
        return tokenTypesType switch
        {
            TokenTypes.Access => DateTime.UtcNow.AddMinutes(_jwtOptions.AccessExpiresMinutes),
            TokenTypes.Refresh => DateTime.UtcNow.AddDays(_jwtOptions.RefreshExpiresDays),
            TokenTypes.EmailConfirmation => DateTime.UtcNow.AddHours(_jwtOptions.EmailConfirmationExpiresHours),
            TokenTypes.ResetPassword => DateTime.UtcNow.AddHours(_jwtOptions.EmailConfirmationExpiresHours),
            TokenTypes.RecoverAccount => DateTime.UtcNow.AddHours(_jwtOptions.RecoverAccountExpiresHours),
            _ => throw new BadRequestException("Invalid token type")
        };
    }

    public int GetTokenExistingTime(TokenTypes tokenTypesType)
    {
        return tokenTypesType switch
        {
            TokenTypes.Access => _jwtOptions.AccessExpiresMinutes,
            TokenTypes.Refresh => _jwtOptions.RefreshExpiresDays,
            TokenTypes.EmailConfirmation => _jwtOptions.EmailConfirmationExpiresHours,
            TokenTypes.ResetPassword => _jwtOptions.EmailConfirmationExpiresHours,
            TokenTypes.RecoverAccount => _jwtOptions.RecoverAccountExpiresHours,
            _ => throw new BadRequestException("Invalid token type")
        };
    }
    
    public TokenModel GenerateTokenModel(UserEntity user, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var result = new TokenModel()
        {
            Id = Guid.NewGuid(),
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            TokenType = tokenType,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = GetExpirationDate(tokenType),
            IsUsed = false,
        };
        cancellationToken.ThrowIfCancellationRequested();
        
        return result;
    }
    
    public TokenModel GenerateTokenModel(UserEntity user, string token, TokenTypes tokenType, CancellationToken cancellationToken)
    {
        var result = new TokenModel()
        {
            Id = Guid.NewGuid(),
            Token = token,
            TokenType = tokenType,
            UserId = user.Id,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = GetExpirationDate(tokenType),
            IsUsed = false,
        };
        cancellationToken.ThrowIfCancellationRequested();
        
        return result;
    }
}