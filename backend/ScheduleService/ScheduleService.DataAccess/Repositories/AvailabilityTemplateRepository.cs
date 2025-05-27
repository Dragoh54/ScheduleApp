using MongoDB.Driver;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class AvailabilityTemplateRepository : BaseRepository<AvailabilityTemplate>, IAvailabilityTemplateRepository
{
    private const string CollectionName = "availability_templates";

    public AvailabilityTemplateRepository(IScheduleDbContext dbContext) : base(dbContext, CollectionName)
    {
    }

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
    
    public async Task SetDefaultTemplateAsync(Guid userId, Guid templateId, CancellationToken cancellationToken)
    {
        DbContext.AddCommand(async () =>
        {
            await Collection.UpdateManyAsync(
                x => x.UserId == userId && x.IsDefault,
                Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, false),
                cancellationToken: cancellationToken);
            
            await Collection.FindOneAndUpdateAsync(
                Builders<AvailabilityTemplate>.Filter.Eq(x => x.Id, templateId),
                Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, true),
                new FindOneAndUpdateOptions<AvailabilityTemplate>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken
            );
        });
        
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}