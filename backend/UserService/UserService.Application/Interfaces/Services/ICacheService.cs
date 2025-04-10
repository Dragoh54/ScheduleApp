using Microsoft.Extensions.Caching.Distributed;
using UserService.DataAccess.Enums;

namespace UserService.Application.Interfaces.Services;

public interface ICacheService<T> where T : class
{
    public Task<T> Get(string key, CancellationToken cancellationToken);

    public Task Set(T entity, string key, CancellationToken cancellationToken,
        DistributedCacheEntryOptions? options = null);
    public Task Set(T entity, string key, TimeSpan absoluteExpiration, CancellationToken cancellationToken);
    public Task Delete(string key, CancellationToken cancellationToken);
    public Task Refresh(string key, CancellationToken cancellationToken);
}