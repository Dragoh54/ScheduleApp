using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class AvailabilityTemplateRepository(
    IScheduleDbContext dbContext
    ) : BaseRepository<AvailabilityTemplate>(dbContext, CollectionName), IAvailabilityTemplateRepository
{
    private const string CollectionName = "availability_templates";

    public async Task<AvailabilityTemplate?> GetDefaultTemplateAsync(Guid userId, CancellationToken cancellationToken)
    {
        var filter = Builders<AvailabilityTemplate>.Filter.And(
            Builders<AvailabilityTemplate>.Filter.Eq(x => x.UserId, userId),
            Builders<AvailabilityTemplate>.Filter.Eq(x => x.IsDefault, true));
        
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AvailabilityTemplate>> GetUserTemplatesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var filter = Builders<AvailabilityTemplate>.Filter.Eq(x => x.UserId, userId);
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }
    
    public async Task<AvailabilityTemplate?> SetDefaultTemplateAsync(Guid userId, Guid templateId, CancellationToken cancellationToken)
    {
        using var session = await DbContext.StartSessionAsync(cancellationToken);
        try
        {
            session.StartTransaction();
            
            await Collection.UpdateManyAsync(
                session,
                x => x.UserId == userId && x.IsDefault,
                Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, false),
                cancellationToken: cancellationToken);

            var result = await Collection.FindOneAndUpdateAsync(
                session,
                Builders<AvailabilityTemplate>.Filter.Eq(x => x.UserId, userId),
                Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, true),
                new FindOneAndUpdateOptions<AvailabilityTemplate>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken
            );

            await session.CommitTransactionAsync(cancellationToken);
            return result;
        }
        catch
        {
            await session.AbortTransactionAsync(cancellationToken);
            throw;
        }
    }
}