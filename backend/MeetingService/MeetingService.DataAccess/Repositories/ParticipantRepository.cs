using MeetingService.DataAccess.Interfaces.Repositories;
using MeetingService.DomainModel.Models;

namespace MeetingService.DataAccess.Repositories;

public class ParticipantRepository(
    MeetingServiceDbContext dbContext
    ) : BaseRepository<Participant>(dbContext), IParticipantRepository
{
    public Task<IEnumerable<Participant>> GetParticipantsByMeetingId(Guid meetingId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Participant?> GetParticipant(Guid meetingId, Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Participant?> GetParticipantByEmail(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}