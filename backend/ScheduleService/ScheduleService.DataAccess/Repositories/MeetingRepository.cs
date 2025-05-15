using MongoDB.Driver;
using ScheduleService.Application.Interfaces.Repositories;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class MeetingRepository : BaseRepository<Meeting>, IMeetingRepository
{
    private const string CollectionName = "meetings";

    public MeetingRepository(IScheduleDbContext dbContext) : base(dbContext, CollectionName)
    {
    }

    public async Task<IEnumerable<Meeting>> GetMeetingsForUserOnDateAsync(Guid userId, DateTime date, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.And(
            Builders<Meeting>.Filter.Eq(m => m.UserId, userId),
            Builders<Meeting>.Filter.Eq(m => m.StartTime.Day, date.Day)
        );
        
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>> GetMeetingsForUserInRangeAsync(Guid userId, DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.And(
            Builders<Meeting>.Filter.Eq(m => m.UserId, userId),
            Builders<Meeting>.Filter.Gte(m => m.StartTime, from),
            Builders<Meeting>.Filter.Lte(m => m.EndTime, to)
        );
        
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>?> GetUserMeetingsAsync(Guid userId, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.Eq(m => m.UserId, userId);
        
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>?> GetUserUpcomingMeetingsAsync(Guid userId, DateTime date, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.And(
            Builders<Meeting>.Filter.Eq(m => m.UserId, userId),
            Builders<Meeting>.Filter.Gte(m => m.StartTime, date));
            
        return await Collection.Find(filter).ToListAsync(cancellationToken);
    }

    //TODO: REWORK (m.Status == MeetingStatus.Scheduled || m.Status == MeetingStatus.Rescheduled))
    //TODO: REMOVE THIS METHOD FROM REPOSITORY
    // public async Task<bool> IsUserHasMeetingAsync(Guid userId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    // {
    //     // var meetings = await Collection.Find(m =>
    //     //     m.UserId == userId  &&
    //     //     (m.Status == MeetingStatus.Scheduled || m.Status == MeetingStatus.Rescheduled))
    //     //     .ToListAsync(cancellationToken);
    //
    //     var overlappedMeetings = meetings.Select(m =>
    //         startTime < m.EndTime && m.StartTime < endTime
    //     );
    //     
    //     return overlappedMeetings.Any();
    // }

    public async Task UpdateMeetingStatusAsync(Guid meetingId, MeetingStatus status, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.Eq(m => m.Id, meetingId);
        var update = Builders<Meeting>.Update.Set(m => m.Status, status);

        DbContext.AddCommand(async () =>
        {
            await Collection.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<Meeting>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken
            );
        });
    }
}