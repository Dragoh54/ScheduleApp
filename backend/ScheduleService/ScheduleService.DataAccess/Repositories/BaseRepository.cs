using MongoDB.Bson;
using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Intefaces;

namespace ScheduleService.DataAccess.Repositories;
public abstract class BaseRepository<T>(IScheduleDbContext dbContext, string collectionName) 
    : IBaseRepository<T> where T : IEntity
{
    protected readonly IMongoCollection<T> Collection = dbContext.GetCollection<T>(collectionName);
    protected readonly IScheduleDbContext DbContext = dbContext;

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, id); 
        return await Collection.Find(dbContext.Session, filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await Collection.Find(dbContext.Session, new BsonDocument()).ToListAsync(cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbContext.AddCommand(() => Collection.InsertOneAsync(dbContext.Session, entity, null, cancellationToken));
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(e => e.Id, entity.Id);
        DbContext.AddCommand(() => Collection.ReplaceOneAsync(dbContext.Session, filter, entity, 
                                      new ReplaceOptions(), cancellationToken));
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        DbContext.AddCommand(() => Collection.DeleteOneAsync(filter, cancellationToken));
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        var count = await Collection.CountDocumentsAsync(
            dbContext.Session, 
            filter, 
            new CountOptions(), 
            cancellationToken);
        return count > 0;
    }
}