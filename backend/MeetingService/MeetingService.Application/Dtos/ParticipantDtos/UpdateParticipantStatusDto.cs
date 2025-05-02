using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.ParticipantDtos;

public record UpdateParticipantStatusDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public ParticipationStatus Status { get; set; }
}