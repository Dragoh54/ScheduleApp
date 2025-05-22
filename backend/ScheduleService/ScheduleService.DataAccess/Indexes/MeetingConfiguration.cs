using MongoDB.Driver;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Indexes;

public static class MeetingConfiguration
{
    public static void ConfigureIndexes(IMongoCollection<Meeting> collection)
    {
        var indexes = Builders<Meeting>.IndexKeys.Ascending(a => a.StartTime);
        var options = new CreateIndexOptions() { Unique = true };
        collection.Indexes.CreateOne(new CreateIndexModel<Meeting>(indexes, options));
    }
}