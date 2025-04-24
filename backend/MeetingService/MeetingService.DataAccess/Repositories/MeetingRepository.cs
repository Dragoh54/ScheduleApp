using MeetingService.DataAccess.Interfaces.Repositories;
using MeetingService.DomainModel.Models;

namespace MeetingService.DataAccess.Repositories;

public class MeetingRepository(
    MeetingServiceDbContext dbContext
    ) : BaseRepository<Meeting>(dbContext), IMeetingRepository
{
    public Task<IEnumerable<Meeting>> GetMeetingsForUser(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Meeting>> GetMeetingsInRange(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Meeting?> GetMeetingWithParticipants(Guid meetingId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}