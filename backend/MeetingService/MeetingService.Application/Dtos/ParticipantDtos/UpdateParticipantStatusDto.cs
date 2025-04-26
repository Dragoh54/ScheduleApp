using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.ParticipantDtos;

public record UpdateParticipantStatusDto
{
    public Guid UserId { get; set; }

    public ParticipationStatus Status { get; set; }
}