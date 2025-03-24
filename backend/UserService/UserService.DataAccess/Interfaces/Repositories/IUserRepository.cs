using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public new Task<IEnumerable<UserEntity>?> Get(CancellationToken cancellationToken);
    public new Task<UserEntity?> Get(Guid id, CancellationToken cancellationToken);
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken);
}