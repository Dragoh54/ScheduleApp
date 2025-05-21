using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Repositories;

public interface IMeetingRepository : IBaseRepository<Meeting>
{
    public Task<IEnumerable<Meeting>> GetMeetingsForUser(Guid userId, CancellationToken cancellationToken);
    
    public Task<IEnumerable<Meeting>> GetMeetingsInRange(DateTime from, DateTime to, CancellationToken cancellationToken);

    public Task<Meeting?> GetMeetingWithParticipants(Guid meetingId, CancellationToken cancellationToken);
}
