using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface ITokenModelRepository : IBaseRepository<TokenModel>
{
    public Task<TokenModel?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    public Task<TokenModel?> GetByToken(string token, CancellationToken cancellationToken);
}