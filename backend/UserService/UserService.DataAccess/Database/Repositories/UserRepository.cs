using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Models;
using UserService.DataAccess.Models.Pagination;
using UserService.DataAccess.Specifications;

namespace UserService.DataAccess.Database.Repositories;

public class UserRepository(
    UserServiceDbContext dbContext
    ) : BaseRepository<UserEntity>(dbContext), IUserRepository
{
    public override async Task<IEnumerable<UserEntity>?> GetAll(CancellationToken cancellationToken)
    {
        var users = await _dbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return users;
    }

    public override async Task<UserEntity?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<(List<UserEntity>?, int)> Get(UserFilters userFilter, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var specification = new UserByFilterSpecification(userFilter);
        var predicate = specification.ToExpression();

        var query = _dbContext.Users
            .AsNoTracking()
            .Where(predicate)
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
        var deletedUsers = await _dbSet
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(u => u.IsDeleted)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return deletedUsers;
    }

    public async Task<UserEntity?> GetDeletedUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var deletedUser = await _dbSet
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.IsDeleted && u.Email == email, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return deletedUser;
    }
}