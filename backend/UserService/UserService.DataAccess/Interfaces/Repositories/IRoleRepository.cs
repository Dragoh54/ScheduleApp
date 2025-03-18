using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    public Task<RoleEntity?> GetByRole(Role role, CancellationToken cancellationToken);
}