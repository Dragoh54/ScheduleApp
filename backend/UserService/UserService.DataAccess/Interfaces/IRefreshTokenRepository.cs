using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IRefreshTokenRepository : IBaseRepository<RefreshToken>
{
    public Task<RefreshToken?> GetByUserId(Guid userId, CancellationToken cancellationToken);
}