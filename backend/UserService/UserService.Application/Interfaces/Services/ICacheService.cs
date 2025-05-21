using Microsoft.Extensions.Caching.Distributed;
using UserService.DataAccess.Enums;

namespace UserService.Application.Interfaces.Services;

public interface ICacheService<T> where T : class
{
    public Task<T?> GetAsync(string key, CancellationToken cancellationToken);

    public Task SetAsync(T entity, string key, CancellationToken cancellationToken,
        DistributedCacheEntryOptions? options = null);
    public Task SetAsync(T entity, string key, TimeSpan absoluteExpiration, CancellationToken cancellationToken);
    public Task SetStringAsync(string entity, string key, CancellationToken cancellationToken,
        DistributedCacheEntryOptions? options = null);
    public Task SetStringAsync(string entity, string key, TimeSpan absoluteExpiration, CancellationToken cancellationToken);
    public Task DeleteAsync(string key, CancellationToken cancellationToken);
    public Task RefreshAsync(string key, CancellationToken cancellationToken);
}