using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface ITokenModelRepository : IBaseRepository<TokenEntity>
{
    /// <summary>
    /// Get token entity by user id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TokenEntity?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get token entity by token string
    /// </summary>
    /// <param name="token"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<TokenEntity?> GetByToken(string token, CancellationToken cancellationToken);
}