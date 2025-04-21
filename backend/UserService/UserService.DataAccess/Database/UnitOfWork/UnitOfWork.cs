using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.DataAccess.Database.UnitOfWork;

public sealed class UnitOfWork(
    UserServiceDbContext dbContext,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ITokenModelRepository tokenModelRepository
    ) : IUnitOfWork
{
    private bool _disposed;
    
    public IUserRepository UserRepository { get; } = userRepository;
    public ITokenModelRepository TokenModelRepository { get; } = tokenModelRepository;
    public IRoleRepository RoleRepository { get; } = roleRepository;

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    private void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            dbContext.Dispose();
        }

        _disposed = true;
    }
}