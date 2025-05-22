using System.Security.Claims;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken);
    public TokenModel GenerateTokenModel(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken);
    public ClaimsPrincipal ValidateToken(string token);
    public DateTime GetExpirationDate(TokenTypes tokenTypesType);
    public int GetTokenExistingTime(TokenTypes tokenTypesType);
}