using MeetingService.DomainModel.Models;

namespace MeetingService.DataAccess.Interfaces.Repositories;

public interface IMeetingRepository : IBaseRepository<Meeting>
{
    Task<IEnumerable<Meeting>> GetMeetingsForUser(Guid userId, CancellationToken cancellationToken);
    
    Task<IEnumerable<Meeting>> GetMeetingsInRange(DateTime from, DateTime to, CancellationToken cancellationToken);

    Task<Meeting?> GetMeetingWithParticipants(Guid meetingId, CancellationToken cancellationToken);
}
