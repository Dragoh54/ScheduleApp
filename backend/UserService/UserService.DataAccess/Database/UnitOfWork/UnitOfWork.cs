using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.DataAccess.Database.UnitOfWork;

public sealed class UnitOfWork(
    UserServiceDbContext dbContext,
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    ITokenModelRepository tokenModelRepository
    ) : IUnitOfWork
{
    private bool _disposed = false;
    
    public IUserRepository UserRepository { get; } = userRepository;

    public ITokenModelRepository TokenModelRepository { get; } = tokenModelRepository;

    public IRoleRepository RoleRepository { get; } = roleRepository;

    public async Task SaveChangesAsync()
    {
        await dbContext.SaveChangesAsync();
    }

    private void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            dbContext.Dispose();
        }

        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}