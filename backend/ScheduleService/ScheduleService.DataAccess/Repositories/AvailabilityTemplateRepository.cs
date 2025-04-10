using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class AvailabilityTemplateRepository(
    IScheduleDbContext dbContext, 
    string collectionName
) : BaseRepository<AvailabilityTemplate>(dbContext, collectionName), IAvailabilityTemplateRepository
{
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

    //TODO: REFACTOR THIS SHIT
    public async Task<AvailabilityTemplate?> SetDefaultTemplateAsync(Guid userId, Guid templateId, CancellationToken cancellationToken)
    {
        await Collection.UpdateManyAsync(
            Builders<AvailabilityTemplate>.Filter.And(
                Builders<AvailabilityTemplate>.Filter.Eq(x => x.UserId, userId),
                Builders<AvailabilityTemplate>.Filter.Eq(x => x.IsDefault, true)
            ),
            Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, false),
            cancellationToken: cancellationToken);
        
        var filter = Builders<AvailabilityTemplate>.Filter.And(
            Builders<AvailabilityTemplate>.Filter.Eq(x => x.UserId, userId),
            Builders<AvailabilityTemplate>.Filter.Eq(x => x.Id, templateId)
        );
        
        var update = Builders<AvailabilityTemplate>.Update
            .Set(x => x.IsDefault, true);

        var options = new FindOneAndUpdateOptions<AvailabilityTemplate>
        {
            ReturnDocument = ReturnDocument.After
        };

        return await Collection.FindOneAndUpdateAsync(
            filter,
            update,
            options,
            cancellationToken);
    }
}