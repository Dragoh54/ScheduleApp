using MongoDB.Driver;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Indexes;

public static class CalendarDayConfiguration
{
    public static void ConfigureIndexes(IMongoCollection<CalendarDay> collection)
    {
        var indexes = Builders<CalendarDay>.IndexKeys.Ascending(a => a.Date);
        var options = new CreateIndexOptions() { Unique = true };
        collection.Indexes.CreateOne(new CreateIndexModel<CalendarDay>(indexes, options));
    }
}