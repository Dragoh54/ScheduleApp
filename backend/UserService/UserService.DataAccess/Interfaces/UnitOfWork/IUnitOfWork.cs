using UserService.DataAccess.Interfaces.Repositories;

namespace UserService.DataAccess.Interfaces.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Repository for working with user entities
    /// </summary>
    public IUserRepository UserRepository { get; }
    
    /// <summary>
    /// Repository for working with token entities
    /// </summary>
    public ITokenModelRepository TokenModelRepository { get; }
    
    /// <summary>
    /// Repository for working with role intities
    /// </summary>
    public IRoleRepository RoleRepository { get; }
    
    /// <summary>
    /// Save all changes in UnitOfWork context
    /// </summary>
    /// <returns></returns>
    public Task SaveChangesAsync();
}