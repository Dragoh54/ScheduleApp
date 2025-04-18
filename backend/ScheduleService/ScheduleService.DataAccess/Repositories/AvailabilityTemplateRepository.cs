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
    }

    public async Task<bool> IsUserFreeAsync(Guid userId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        var template = await Collection
            .Find(x => x.UserId == userId && x.IsDefault)
            .FirstOrDefaultAsync(cancellationToken);

        var daySchedule = template?.Schedule.FirstOrDefault(s => s.DayOfWeek - 1 == startTime.DayOfWeek);
        if (daySchedule == null)
            return false;

        var start = TimeOnly.FromDateTime(startTime);
        var end = TimeOnly.FromDateTime(endTime);

        return daySchedule.TimeSlots.Any(slot =>
            slot.StartTime <= start && slot.EndTime >= end);
    }


    //TODO: FIX THIS METHOD (IT SAVES OLD DEFAULTS)
    // public async Task<AvailabilityTemplate?> SetDefaultTemplateAsync(Guid userId, Guid templateId, CancellationToken cancellationToken)
    // {
    //     AvailabilityTemplate? updatedTemplate = null;
    //
    //     DbContext.AddCommand(async () =>
    //     {
    //         await Collection.UpdateManyAsync(
    //             x => x.UserId == userId && x.IsDefault,
    //             Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, false),
    //             cancellationToken: cancellationToken);
    //         
    //         updatedTemplate = await Collection.FindOneAndUpdateAsync(
    //             Builders<AvailabilityTemplate>.Filter.Eq(x => x.Id, templateId),
    //             Builders<AvailabilityTemplate>.Update.Set(x => x.IsDefault, true),
    //             new FindOneAndUpdateOptions<AvailabilityTemplate>
    //             {
    //                 ReturnDocument = ReturnDocument.After
    //             },
    //             cancellationToken
    //         );
    //     });
    //     
    //     return updatedTemplate;
    // }
}