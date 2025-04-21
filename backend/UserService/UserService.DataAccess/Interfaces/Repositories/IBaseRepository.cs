namespace UserService.DataAccess.Interfaces.Repositories;

public interface IBaseRepository<T>
{
    /// <summary>
    /// Get all entities to database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
    
    /// <summary>
    /// Get entity by id to database
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> GetById(Guid id, CancellationToken cancellationToken);
    
    /// <summary>
    /// Add entity to database
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T> Add(T item, CancellationToken cancellationToken);
    
    /// <summary>
    /// Update entity in database
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<T?> Update(T item, CancellationToken cancellationToken);
    
    /// <summary>
    /// Save changes to database
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task SaveAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Delete entity from database
    /// </summary>
    /// <param name="item"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> Delete(T item, CancellationToken cancellationToken);
}