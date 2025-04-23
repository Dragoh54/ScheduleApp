using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Interfaces;

namespace MeetingService.DomainModel.Models;

public class Participant : IdEntity
{
    public Guid MeetingId { get; set; }
    public Guid UserId { get; set; }
    public ParticipationStatus Status { get; set; }
}   