using UserService.DataAccess.Models;
using UserService.DataAccess.Pagination;

namespace UserService.DataAccess.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    /// <summary>
    /// Get user entities, including UserRoles entities with Roles
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<UserEntity>?> GetAllWithRolesAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Get user entity, including UserRoles entities with Roles
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get paginated user entities, including UserRoles with Roles
    /// </summary>
    /// <param name="userFilter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<(List<UserEntity>?, int)> GetFilteredWithRoles(UserFilters userFilter, int pageNumber, int pageSize, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get user entity with tracking
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetWithTrackingAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get user entity by email, including UserRoles with Roles
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get user entities which are deleted
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Get deleted user entity by email
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<UserEntity?> GetDeletedUserByEmailAsync(string email, CancellationToken cancellationToken);
}