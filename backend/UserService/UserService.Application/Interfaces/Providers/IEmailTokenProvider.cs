using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.Application.Interfaces.Providers;

public interface IEmailTokenProvider
{
    public TokenEntity GenerateTokenEntity(UserEntity user, TokenTypes tokenType, CancellationToken cancellationToken);
    public string GenerateEmailToken(UserEntity user, TokenTypes tokenTypesType, CancellationToken cancellationToken);
    public int GetTokenExistingTime(TokenTypes tokenTypesType);
}