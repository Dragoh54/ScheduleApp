using MongoDB.Driver;
using ScheduleService.DataAccess.Interfaces.DbContext;
using ScheduleService.DataAccess.Interfaces.Repositories;
using ScheduleService.DomainModel.Enums;
using ScheduleService.DomainModel.Models;

namespace ScheduleService.DataAccess.Repositories;

public class MeetingRepository(
    IScheduleDbContext dbContext
) : BaseRepository<Meeting>(dbContext, CollectionName), IMeetingRepository
{
    private const string CollectionName = "meetings";
    
    public async Task<IEnumerable<Meeting>> GetMeetingsForUserOnDateAsync(Guid userId, DateOnly date, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.And(
            Builders<Meeting>.Filter.Eq(m => m.UserId, userId),
            Builders<Meeting>.Filter.Eq(m => DateOnly.FromDateTime(m.StartTime), date)
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
    
    //TODO: REWORK (m.Status == MeetingStatus.Scheduled || m.Status == MeetingStatus.Rescheduled))
    public async Task<bool> IsUserHasMeetingAsync(Guid userId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
    {
        var meetings = await Collection.Find(m =>
            m.UserId == userId  &&
            (m.Status == MeetingStatus.Scheduled || m.Status == MeetingStatus.Rescheduled))
            .ToListAsync(cancellationToken);

        var overlappedMeetings = meetings.Select(m =>
            startTime < m.EndTime && m.StartTime < endTime
        );
        
        return overlappedMeetings.Any();
    }

    public async Task<Meeting?> UpdateMeetingStatusAsync(Guid meetingId, MeetingStatus status, CancellationToken cancellationToken)
    {
        var filter = Builders<Meeting>.Filter.Eq(m => m.Id, meetingId);
        var update = Builders<Meeting>.Update.Set(m => m.Status, status);

        Meeting? updatedMeeting = null;

        DbContext.AddCommand(async () =>
        {
            updatedMeeting = await Collection.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<Meeting>
                {
                    ReturnDocument = ReturnDocument.After
                },
                cancellationToken
            );
        });

        return updatedMeeting;
    }
}