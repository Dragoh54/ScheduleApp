using MeetingService.DataAccess.Interfaces.Repositories;
using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess.Repositories;

public class MeetingRepository(
    MeetingServiceDbContext dbContext
    ) : BaseRepository<Meeting>(dbContext), IMeetingRepository
{
    public async Task<IEnumerable<Meeting>> GetMeetingsForUser(Guid userId, CancellationToken cancellationToken)
    {
        return await DbContext.Meetings
            .Where(m => m.OrganizationUserId == userId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Meeting>> GetMeetingsInRange(DateTime from, DateTime to, CancellationToken cancellationToken)
    {
        return await DbContext.Meetings
            .Where(m => m.StartTime >= from && m.EndTime <= to)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Meeting?> GetMeetingWithParticipants(Guid meetingId, CancellationToken cancellationToken)
    {
        return await DbContext.Meetings
            .Include(m => m.Participants)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == meetingId, cancellationToken);
    }
}