using MeetingService.DataAccess.Interfaces.Repositories;
using MeetingService.DomainModel.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingService.DataAccess.Repositories;

public class ParticipantRepository(
    MeetingServiceDbContext dbContext
    ) : BaseRepository<Participant>(dbContext), IParticipantRepository
{
    public async Task<IEnumerable<Participant>> GetParticipantsByMeetingId(Guid meetingId, CancellationToken cancellationToken)
    {
        return await _dbContext.Participants
            .Where(p => p.MeetingId == meetingId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Participant?> GetParticipant(Guid meetingId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.MeetingId == meetingId && p.UserId == userId, cancellationToken);
    }

    public async Task<Participant?> GetParticipantWithMeeting(Guid meetingId, Guid userId, CancellationToken cancellationToken)
    {
        return await _dbContext.Participants
            .Include(p => p.Meeting)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.MeetingId == meetingId && p.UserId == userId, cancellationToken);
    }

    public async Task<Participant?> GetParticipantByEmail(Guid meetingId, string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Participants
            .AsNoTracking()
            .FirstOrDefaultAsync(p => 
                p.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) &&
                p.MeetingId == meetingId, cancellationToken);
    }
}