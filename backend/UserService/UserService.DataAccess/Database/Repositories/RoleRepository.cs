using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
{

    public RoleRepository(UserServiceDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<RoleEntity>?> GetAllWithUserAsync(CancellationToken cancellationToken)
    {
        var roles = await DbContext.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(ur => ur.User)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return roles;
    }

    public async Task<RoleEntity?> GetByIdWithUserAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await DbContext.Roles
            .Include(r => r.UserRoles) 
            .ThenInclude(ur => ur.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return user;
    }

    public async Task<RoleEntity?> GetByRoleAsync(Roles roles, CancellationToken cancellationToken)
    {
        var role = await DbContext.Roles
            .Include(r => r.UserRoles)
            .ThenInclude(ur => ur.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.RoleName == roles, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return role;
    }
}