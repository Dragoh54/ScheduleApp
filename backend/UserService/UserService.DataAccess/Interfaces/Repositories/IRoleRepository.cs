using UserService.DataAccess.Enums;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface IRoleRepository : IBaseRepository<RoleEntity>
{
    /// <summary>
    /// Get all roles entities, including UserRoles with User entity
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<RoleEntity>?> GetAll(CancellationToken cancellationToken);
    
    /// <summary>
    /// Get role entity, including UserRoles with User
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<RoleEntity?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get role entity by role name fom Roles enum
    /// </summary>
    /// <param name="roles"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<RoleEntity?> GetByRole(Roles roles, CancellationToken cancellationToken);
}