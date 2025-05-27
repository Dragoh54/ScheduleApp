namespace MeetingService.Application.Dtos.ParticipantDto.Requests;

public record RemoveParticipantFromMeetingRequestDto
{
    public Guid UserId { get; set; }
}