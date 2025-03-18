using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class RefreshTokenRepository : BaseRepository<RefreshToken>, IRefreshTokenRepository
{
    public RefreshTokenRepository(UserServiceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<RefreshToken?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var token = await _dbContext.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.UserId == userId, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }
}