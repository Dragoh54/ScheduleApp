using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Models;
using UserService.DataAccess.Pagination;
using UserService.DataAccess.Specifications;

namespace UserService.DataAccess.Database.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(UserServiceDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<UserEntity>?> GetAllWithRolesAsync(CancellationToken cancellationToken)
    {
        var users = await DbContext.Users
            .Include(u => u.UserRoles)
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return users;
    }

    public async Task<UserEntity?> GetByIdWithRolesAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await DbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<(List<UserEntity>?, int)> GetFilteredWithRoles(UserFilters userFilter, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var specification = new UserByFilterSpecification(userFilter);
        var predicate = specification.ToExpression();

        var query = DbContext.Users
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


    public async Task<UserEntity?> GetWithTrackingAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await DbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await DbContext.Users
            .Include(u => u.UserRoles) 
            .ThenInclude(ur => ur.Role)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<IEnumerable<UserEntity>?> GetDeletedUsersAsync(CancellationToken cancellationToken)
    {
        var deletedUsers = await DbSet
            .AsNoTracking()
            .IgnoreQueryFilters()
            .Where(u => u.IsDeleted)
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return deletedUsers;
    }

    public async Task<UserEntity?> GetDeletedUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var deletedUser = await DbSet
            .AsNoTracking()
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.IsDeleted && u.Email == email, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return deletedUser;
    }
}