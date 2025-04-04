using Microsoft.EntityFrameworkCore;
using ScheduleService.DataAccess.DatabaseContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class BaseRepository<T> (
    ScheduleServiceDbContext dbContext
    ) : IBaseRepository<T> where T : GenericEntity
{
    private readonly ScheduleServiceDbContext _dbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public async Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken)
    {
        var res = await _dbSet
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        return res;
    }

    public async Task<T?> Get(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id.Equals(id), cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return entity;
    }

    public async Task<T> Add(T item, CancellationToken cancellationToken)
    {
        var result = await _dbSet.AddAsync(item, cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();

        return result.Entity;
    }

    public async Task<T?> Update(T item, CancellationToken cancellationToken)
    {
        _dbSet.Update(item);
        cancellationToken.ThrowIfCancellationRequested();
        
        return item;
    }

    public async Task SaveAsync(CancellationToken cancellationToken)
    {
        await _dbContext.SaveChangesAsync(cancellationToken);
        cancellationToken.ThrowIfCancellationRequested();
    }

    public async Task<bool> Delete(T item, CancellationToken cancellationToken)
    {
        _dbSet.Remove(item);
        cancellationToken.ThrowIfCancellationRequested();

        return true;
    }
}