using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class TokenModelModelRepository : BaseRepository<TokenEntity>, ITokenModelRepository
{
    public TokenModelModelRepository(UserServiceDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<TokenEntity?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var token = await DbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.UserId == userId, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }

    public async Task<TokenEntity?> GetByToken(string token, CancellationToken cancellationToken)
    {
        var result = await DbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return result;
    }
}