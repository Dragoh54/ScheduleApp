using ScheduleService.DomainModel.Intefaces;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Interfaces.Repositories;

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
    /// Get all entities 
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// add entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public Task AddAsync(T entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken"></param>
    public Task UpdateAsync(T entity, CancellationToken cancellationToken);
    
    /// <summary>
    /// delete entity
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// check if entity exist by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken);
}