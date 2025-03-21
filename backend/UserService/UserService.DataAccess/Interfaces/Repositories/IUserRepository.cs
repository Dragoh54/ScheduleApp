using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    public Task<IEnumerable<UserEntity>?> GetAllWithRoles(CancellationToken cancellationToken);
    public Task<UserEntity?> GetWithRolesAsync(Guid id, CancellationToken cancellationToken);
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken);
}