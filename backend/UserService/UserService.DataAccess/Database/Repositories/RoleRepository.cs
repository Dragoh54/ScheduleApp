using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class RoleRepository(UserServiceDbContext dbContext) : BaseRepository<RoleEntity>(dbContext), IRoleRepository
{
    public async Task<IEnumerable<RoleEntity>?> Get(CancellationToken cancellationToken)
    {
        var roles = await _dbContext.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(ur => ur.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
        
        return roles;
    }

    public new async Task<RoleEntity?> Get(Guid id, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Roles
            .Include(r => r.UserRoles) 
            .ThenInclude(ur => ur.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<RoleEntity?> GetByRole(Role role, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(ur => ur.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleName == role, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return item;
    }
}