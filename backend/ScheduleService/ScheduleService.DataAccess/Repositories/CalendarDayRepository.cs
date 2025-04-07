using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class CalendarDayRepository(IMongoDatabase database, string collectionName) 
    : BaseRepository<CalendarDay>(database, collectionName), ICalendarDayRepository
{
    public async Task<CalendarDay?> GetByDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken)
    {
        var filter = Builders<CalendarDay>.Filter.And(
            Builders<CalendarDay>.Filter.Eq(x => x.UserId, userId),
            Builders<CalendarDay>.Filter.Eq(x => x.Date, date));
        
        return await Collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IEnumerable<CalendarDay>> GetForPeriodAsync(Guid userId, DateOnly start, DateOnly end, CancellationToken cancellationToken)
    {
        var filter = Builders<CalendarDay>.Filter.And(
                Builders<CalendarDay>.Filter.Eq(x => x.UserId, userId),
                Builders<CalendarDay>.Filter.Gte(x => x.Date, start),
                Builders<CalendarDay>.Filter.Lte(x => x.Date, end));
        
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }
}