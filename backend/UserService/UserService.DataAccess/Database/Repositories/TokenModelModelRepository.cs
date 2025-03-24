using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class TokenModelModelRepository : BaseRepository<TokenModel>, ITokenModelRepository
{
    public TokenModelModelRepository(UserServiceDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<TokenModel?> GetByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var token = await _dbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.UserId == userId, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return token;
    }

    public async Task<TokenModel?> GetByToken(string token, CancellationToken cancellationToken)
    {
        var result = await _dbContext.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == token, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return result;
    }
}