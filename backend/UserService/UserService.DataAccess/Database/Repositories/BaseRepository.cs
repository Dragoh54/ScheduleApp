using Microsoft.EntityFrameworkCore;
using UserService.DataAccess.Interfaces.Repositories;
using UserService.DataAccess.Models;

namespace UserService.DataAccess.Database.Repositories;

public class BaseRepository<T> : IBaseRepository<T> 
    where T : IdEntity
{
    protected readonly UserServiceDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    protected BaseRepository(UserServiceDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await DbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entities;
    }

    public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id.Equals(id), cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entity;
    }

    public virtual async Task<T> AddAsync(T item, CancellationToken cancellationToken)
    {
        var addedEntity = await DbSet.AddAsync(item, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        await DbContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity;
    }

    public virtual async Task<T?> UpdateAsync(T item, CancellationToken cancellationToken)
    {
        DbSet.Update(item);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return item;
    }

    public virtual async Task<bool> DeleteAsync(T item, CancellationToken cancellationToken)
    {
        DbSet.Remove(item);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        await DbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}