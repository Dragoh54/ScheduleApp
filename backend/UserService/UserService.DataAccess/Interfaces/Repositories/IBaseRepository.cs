namespace UserService.DataAccess.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    /// <summary>
    /// Get all entities to database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Get entity by id to database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Add entity to database
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> AddAsync(T item, CancellationToken cancellationToken);
    
    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> UpdateAsync(T item, CancellationToken cancellationToken);
    
    /// <summary>
    /// Delete entity from database
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> DeleteAsync(T item, CancellationToken cancellationToken);
}