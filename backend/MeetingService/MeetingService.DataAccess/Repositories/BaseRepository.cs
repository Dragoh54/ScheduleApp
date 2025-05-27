using Dapper;
using MeetingService.Application.Interfaces.Repositories;
using MeetingService.DomainModel.Interfaces;
using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess.Repositories;

public class BaseRepository<T> : IBaseRepository<T> 
    where T : IdEntity
{
    protected readonly MeetingServiceDbContext DbContext;
    protected readonly DbSet<T> DbSet;

    protected BaseRepository(MeetingServiceDbContext dbContext)
    {
        DbContext = dbContext;
        DbSet = DbContext.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        var entities = await DbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entities;
    }

    public virtual async Task<T?> GetById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await DbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id.Equals(id), cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entity;
    }

    public virtual async Task<T> Add(T item, CancellationToken cancellationToken)
    {
        var addedEntity = await DbSet.AddAsync(item, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        await DbContext.SaveChangesAsync(cancellationToken);

        return addedEntity.Entity;
    }

    public virtual async Task<T?> Update(T item, CancellationToken cancellationToken)
    {
        DbSet.Update(item);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        await DbContext.SaveChangesAsync(cancellationToken);
        
        return item;
    }

    public virtual async Task<bool> Delete(T item, CancellationToken cancellationToken)
    {
        DbSet.Remove(item);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        await DbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}