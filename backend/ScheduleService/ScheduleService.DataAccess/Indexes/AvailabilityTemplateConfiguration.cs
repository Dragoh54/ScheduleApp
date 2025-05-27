using MongoDB.Driver;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Indexes;

public static class AvailabilityTemplateConfiguration
{
    public static void ConfigureIndexes(IMongoCollection<AvailabilityTemplate> collection)
    {
        var indexes = Builders<AvailabilityTemplate>.IndexKeys.Ascending(a => a.Name);
        var options = new CreateIndexOptions() { Unique = true };
        collection.Indexes.CreateOne(new CreateIndexModel<AvailabilityTemplate>(indexes, options));
    }
}