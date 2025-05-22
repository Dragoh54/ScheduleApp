using System.Linq.Expressions;
using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : IEntity
{
    /// <summary>
    /// Find in collection item by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Get all entities filtered by filter query
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken);
    
    /// <summary>
    /// add entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public Task<T?> AddAsync(T entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public Task<T?> UpdateAsync(T entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// check if entity exist by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}