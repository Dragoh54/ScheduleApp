using Microsoft.EntityFrameworkCore;
using UserService.Application.Dto;
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

    public async Task<(List<UserEntity>?, int)> Get(Filters filter, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .AsNoTracking()
            .Where(a => string.IsNullOrEmpty(filter.Username) || a.Username.Contains(filter.Username))
            .Where(a => string.IsNullOrEmpty(filter.Email) || a.Email.Contains(filter.Email))
            .Where(a => string.IsNullOrEmpty(filter.FirstName) || a.FirstName.Contains(filter.FirstName))
            .Where(a => string.IsNullOrEmpty(filter.LastName) || a.LastName.Contains(filter.LastName))
            .Where(a => string.IsNullOrEmpty(filter.LastLoginAt) || a.LastLoginAt.Date == DateTime.Parse(filter.LastLoginAt).Date)
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsQueryable();
        
        cancellationToken.ThrowIfCancellationRequested();

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .OrderBy(u => u.LastName) 
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return (items, totalCount);
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
            .Where(u => u.IsDeleted)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        return res;
    }

    public async Task<UserEntity?> GetDeletedUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var res = await _dbSet
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.IsDeleted && u.Email == email, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        return res;
    }
}