using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Auth;

public interface IJwtProvider
{
    public string GenerateAccessToken(UserEntity user, CancellationToken cancellationToken);
    public RefreshToken GenerateRefreshToken(UserEntity user, CancellationToken cancellationToken);
}