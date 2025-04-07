using MongoDB.Bson;
using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DataAccess.Repositories;

public class BaseRepository<T>(
    IMongoDatabase database,
    string collectionName
    ) : IBaseRepository<T> where T : IEntity
{
    protected readonly IMongoCollection<T> Collection = database.GetCollection<T>(collectionName);
    
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id);
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }
    
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await Collection.Find(new BsonDocument()).ToListAsync(cancellationToken);
    }
    
    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await Collection.InsertOneAsync(entity, null, cancellationToken);
    }
    
    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        await Collection.ReplaceOneAsync(filter, entity, new ReplaceOptions(), cancellationToken);
    }
    
    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        await Collection.DeleteOneAsync(filter, cancellationToken);
    }
    
    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        return await Collection.CountDocumentsAsync(filter, new CountOptions(), cancellationToken) > 0;
    }
}