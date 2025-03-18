using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Interfaces.UnitOfWork;

namespace UserService.DataAccess.Database.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly UserServiceDbContext _dbContext;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    
    private bool _disposed = false;
    
    public IUserRepository UserRepository => _userRepository;
    public IRefreshTokenRepository RefreshTokenRepository => _refreshTokenRepository;
    public IRoleRepository RoleRepository => _roleRepository;

    public UnitOfWork(UserServiceDbContext dbContext, IUserRepository userRepository, IRoleRepository roleRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (!this._disposed && disposing)
        {
            _dbContext.Dispose();
        }

        this._disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}