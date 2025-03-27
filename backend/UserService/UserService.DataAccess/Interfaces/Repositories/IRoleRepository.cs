using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    public Task<IEnumerable<RoleEntity>?> Get(CancellationToken cancellationToken);
    public new Task<RoleEntity?> Get(Guid id, CancellationToken cancellationToken);
    public Task<RoleEntity?> GetByRole(Role role, CancellationToken cancellationToken);
}