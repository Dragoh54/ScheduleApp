using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Interfaces.Repositories;

public interface IBaseRepository<T> where T : GenericEntity
{
    public Task<IEnumerable<T>> GetAll(CancellationToken cancellationToken);
    public Task<T?> Get(Guid id, CancellationToken cancellationToken);
    public Task<T> Add(T item, CancellationToken cancellationToken);
    public Task<T?> Update(T item, CancellationToken cancellationToken);
    public Task SaveAsync(CancellationToken cancellationToken);
    public Task<bool> Delete(T item, CancellationToken cancellationToken);
}