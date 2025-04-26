using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DomainModel.Exceptions;
using Microsoft.Extensions.Caching.Distributed;

namespace MeetingService.Application.Services;

public class CacheService<T>(
    IDistributedCache cache
    ) : ICacheService<T> where T : class
{
    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        WriteIndented = false,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public async Task<T> Get(string key, CancellationToken cancellationToken)
    {
        try
        {
            var entityBytes = await cache.GetAsync(key, cancellationToken);

            var entityJson = Encoding.UTF8.GetString(entityBytes);
            var entity = JsonSerializer.Deserialize<T>(entityJson, _jsonOptions)
                         ?? throw new BadRequestException("Deserialized entity is null");
            
            return entity;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to deserialize cached data", ex);
        }
    }

    public async Task Set(T entity, string key, CancellationToken cancellationToken, DistributedCacheEntryOptions? options = null)
    {
        try
        {
            var entityJson = JsonSerializer.Serialize(entity, _jsonOptions);
            var entityBytes = Encoding.UTF8.GetBytes(entityJson);
            
            await cache.SetAsync(
                key, 
                entityBytes, 
                options ?? new DistributedCacheEntryOptions(), 
                cancellationToken);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException("Failed to serialize entity for caching", ex);
        }
    }

    public async Task Set(T entity, string key, TimeSpan absoluteExpiration, CancellationToken cancellationToken)
    {
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = absoluteExpiration
        };
        
        await Set(entity, key, cancellationToken, cacheOptions);
    }

    public async Task Delete(string key, CancellationToken cancellationToken)
    {
        try
        {
            await cache.RemoveAsync(key, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to remove cached entity", ex);
        }
    }

    public async Task Refresh(string key, CancellationToken cancellationToken)
    {
        try
        {
            await cache.RefreshAsync(key, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to refresh cached entity", ex);
        }
    }
}