using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface ITokenModelRepository : IBaseRepository<TokenModel>
{
    public Task<TokenModel?> GetByUserId(Guid userId, CancellationToken cancellationToken);
    public Task<TokenModel?> GetByToken(string token, CancellationToken cancellationToken);
}