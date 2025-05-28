using MeetingService.Application.Interfaces.Repositories;
using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess.Repositories;

public class ScheduledJobRepository : BaseRepository<ScheduledJob>, IScheduledJobRepository
{
    public ScheduledJobRepository(MeetingServiceDbContext dbContext) : base(dbContext)
    {
    }
    
    public async Task<IEnumerable<ScheduledJob>> GetScheduledJobsByMeetingId(Guid meetingId, CancellationToken cancellationToken)
    {
        return await DbContext.ScheduledJobs
            .Where(sc => sc.MeetingId == meetingId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<ScheduledJob?> GetScheduledJobByJobId(string jobId, CancellationToken cancellationToken)
    {
        return await DbContext.ScheduledJobs
            .AsNoTracking()
            .FirstOrDefaultAsync(sc => sc.JobId == jobId, cancellationToken);
        throw new NotImplementedException();
    }
}