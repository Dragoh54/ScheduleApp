using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Interfaces.Repositories;

public interface IParticipantRepository : IBaseRepository<Participant>
{
    public Task<IEnumerable<Participant>> GetParticipantsByMeetingId(Guid meetingId, CancellationToken cancellationToken);

    public Task<Participant?> GetParticipant(Guid meetingId, Guid userId, CancellationToken cancellationToken);
    
    public Task<Participant?> GetParticipantWithMeeting(Guid meetingId, Guid userId, CancellationToken cancellationToken);

    public Task<Participant?> GetParticipantByEmail(Guid meetingId, string email, CancellationToken cancellationToken);
}
