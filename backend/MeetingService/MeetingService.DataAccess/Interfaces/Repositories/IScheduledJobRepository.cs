using MeetingService.DomainModel.Models;

namespace MeetingService.DataAccess.Interfaces.Repositories;

public interface IScheduledJobRepository : IBaseRepository<ScheduledJob>
{
    public Task<IEnumerable<ScheduledJob>> GetScheduledJobsByMeetingId(Guid meetingId, CancellationToken cancellationToken);
    public Task<ScheduledJob?> GetScheduledJobByJobId(string jobId, CancellationToken cancellationToken);
}