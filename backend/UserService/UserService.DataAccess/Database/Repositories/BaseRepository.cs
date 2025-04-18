using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class BaseRepository<T> : IBaseRepository<T> 
    where T : IdEntity
{
    protected readonly UserServiceDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(UserServiceDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> Get(CancellationToken cancellationToken)
    {
        var entities = await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entities;
    }

    public virtual async Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id.Equals(id), cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entity;
    }

    public virtual async Task<T> Add(T item, CancellationToken cancellationToken)
    {
        var addedEntity = await _dbSet.AddAsync(item, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        return addedEntity.Entity;
    }

    public virtual async Task<T?> Update(T item, CancellationToken cancellationToken)
    {
        _dbSet.Update(item);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return item;
    }

    public virtual async Task<bool> Delete(T item, CancellationToken cancellationToken)
    {
        _dbSet.Remove(item);
        
        cancellationToken.ThrowIfCancellationRequested();

        return true;
    }
    
    public virtual async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
    }
}