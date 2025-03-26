using System.Security.Claims;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity user, Token tokenType, CancellationToken cancellationToken);
    public TokenModel GenerateTokenModel(UserEntity user, Token tokenType, CancellationToken cancellationToken);
    public ClaimsPrincipal ValidateToken(string token);
}