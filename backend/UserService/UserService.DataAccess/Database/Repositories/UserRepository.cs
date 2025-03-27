using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class UserRepository(UserServiceDbContext dbContext) : BaseRepository<UserEntity>(dbContext), IUserRepository
{
    public async Task<IEnumerable<UserEntity>?> Get(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        return users;
    }

    public new async Task<UserEntity?> Get(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }
    
    public async Task<UserEntity?> GetWithTracking(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken)
    {
        var res = await _dbSet
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(u => !u.IsDeleted)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        return res;
    }
}