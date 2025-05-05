using MeetingService.DomainModel.Enums;

namespace MeetingService.Application.Dtos.ParticipantDtos;

public record ConfirmParticipantStatusDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string ParticipationStatusString { get; set; }
}