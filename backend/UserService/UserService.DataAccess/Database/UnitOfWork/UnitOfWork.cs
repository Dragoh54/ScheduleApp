using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.DataAccess.Database.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly UserServiceDbContext _dbContext;
    
    private bool _disposed;

    public UnitOfWork(UserServiceDbContext dbContext, IUserRepository userRepository, IRoleRepository roleRepository,
        ITokenModelRepository tokenModelRepository)
    {
        _dbContext = dbContext;
        
        UserRepository = userRepository;
        TokenModelRepository = tokenModelRepository;
        RoleRepository = roleRepository;
    }
    
    public IUserRepository UserRepository { get; }
    public ITokenModelRepository TokenModelRepository { get; }
    public IRoleRepository RoleRepository { get; }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
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
            _dbContext.Dispose();
        }

        _disposed = true;
    }
}