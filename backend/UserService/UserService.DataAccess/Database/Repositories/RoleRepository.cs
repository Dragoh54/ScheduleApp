using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class RoleRepository : BaseRepository<RoleEntity>, IRoleRepository
{
    public RoleRepository(UserServiceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RoleEntity?> GetByRole(Role role, CancellationToken cancellationToken)
    {
        var item = await _dbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Role == role, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return item;
    }
}