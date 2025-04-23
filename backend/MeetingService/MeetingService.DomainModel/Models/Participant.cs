using MeetingService.DomainModel.Enums;

namespace MeetingService.DomainModel.Models;

public class Participant
{
    public Guid Id { get; set; }
    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    public ParticipationStatus Status { get; set; }
}   