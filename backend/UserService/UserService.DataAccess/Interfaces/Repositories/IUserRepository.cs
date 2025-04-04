using UserService.DataAccess.Models;
using UserService.DataAccess.Models.Pagination;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<IEnumerable<UserEntity>?> Get(CancellationToken cancellationToken);
    public new Task<UserEntity?> Get(Guid id, CancellationToken cancellationToken);
    public Task<(List<UserEntity>?, int)> Get(UserFilters userFilter, int pageNumber, int pageSize, CancellationToken cancellationToken);
    public Task<UserEntity?> GetWithTracking(Guid id, CancellationToken cancellationToken);
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    public Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken);
    public Task<UserEntity?> GetDeletedUserByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<IEnumerable<UserEntity?>> GetOldUsersAsync(DateTime ago, CancellationToken cancellationToken);
}