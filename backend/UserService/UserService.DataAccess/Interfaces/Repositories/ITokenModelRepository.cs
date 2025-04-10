using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface ITokenModelRepository : IBaseRepository<TokenEntity>
{
    public Task<TokenEntity?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    public Task<TokenEntity?> GetByToken(string token, CancellationToken cancellationToken);
}