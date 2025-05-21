using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using UserService.Application.Interfaces.Auth;
using UserService.Application.Interfaces.Services;
using UserService.DataAccess.Enums;
using UserService.DataAccess.Exceptions;
using JsonException = Newtonsoft.Json.JsonException;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace UserService.Application.Services;

public class CacheService<T> : ICacheService<T> where T : class
{
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _jsonOptions;
    
    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
        _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = false,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
    }
    
    public async Task<T> Get(string key, CancellationToken cancellationToken)
    {
        try
        {
            var entityBytes = await _cache.GetAsync(key, cancellationToken);

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
            
            await _cache.SetAsync(
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
            await _cache.RemoveAsync(key, cancellationToken);
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
            await _cache.RefreshAsync(key, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to refresh cached entity", ex);
        }
    }
}