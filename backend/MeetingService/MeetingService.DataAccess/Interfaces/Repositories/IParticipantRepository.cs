using MeetingService.DomainModel.Models;

namespace MeetingService.DataAccess.Interfaces.Repositories;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    public Task<IEnumerable<Participant>> GetParticipantsByMeetingId(Guid meetingId, CancellationToken cancellationToken);

    public Task<Participant?> GetParticipant(Guid meetingId, Guid userId, CancellationToken cancellationToken);

    public Task<Participant?> GetParticipantByEmail(string email, CancellationToken cancellationToken);
}
