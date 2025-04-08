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
        
        return await Collection.Find(dbContext.Session, filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<AvailabilityTemplate>> GetUserTemplatesAsync(Guid userId, CancellationToken cancellationToken)
    {
        var filter = Builders<AvailabilityTemplate>.Filter.Eq(x => x.UserId, userId);
        return await Collection.Find(dbContext.Session, filter).ToListAsync(cancellationToken);
    }
}