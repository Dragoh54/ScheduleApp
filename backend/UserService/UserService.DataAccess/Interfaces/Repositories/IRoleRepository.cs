using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    public Task<IEnumerable<RoleEntity>?> Get(CancellationToken cancellationToken);
    public Task<RoleEntity?> Get(Guid id, CancellationToken cancellationToken);
    public Task<RoleEntity?> GetByRole(Roles roles, CancellationToken cancellationToken);
}