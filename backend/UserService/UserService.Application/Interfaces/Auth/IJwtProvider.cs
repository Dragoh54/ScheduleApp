using System.Security.Claims;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken);
    public TokenEntity GenerateTokenModel(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken);
    public Task<string> GetClaimFromToken(string token, string claimType, CancellationToken cancellationToken);
    public ClaimsPrincipal ValidateToken(string token);
    public DateTime GetExpirationDate(TokenTypes tokenTypesType);
    public int GetTokenExistingTime(TokenTypes tokenTypesType);
    public string GenerateRefreshTokenString();
}