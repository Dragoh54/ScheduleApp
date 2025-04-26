namespace MeetingService.Application.Dtos.ParticipantDtos;

public record RemoveParticipantFromMeetingDto
{
    public Guid UserId { get; set; }
}