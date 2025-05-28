using System.Security.Claims;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.Application.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateAccessToken(UserEntity user, CancellationToken cancellationToken);
    public TokenEntity GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken);
    public ClaimsPrincipal ValidateToken(string token);
    public Task<string> GetClaimFromToken(string token, string claimType);
    public string GenerateRefreshTokenString();
}