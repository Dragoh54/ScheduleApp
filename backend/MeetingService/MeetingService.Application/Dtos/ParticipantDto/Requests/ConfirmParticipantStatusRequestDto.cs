namespace MeetingService.Application.Dtos.ParticipantDto.Requests;

public record ConfirmParticipantStatusRequestDto
{
    public string Email { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string ParticipationStatusString { get; set; } = string.Empty;
}