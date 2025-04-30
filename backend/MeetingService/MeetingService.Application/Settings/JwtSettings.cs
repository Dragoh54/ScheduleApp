namespace MeetingService.Application.Settings;

public record JwtSettings
{
    public int ParticipantConfirmationExpiresHours { get; set; }
    public int ParticipantStatusChangedExpiresHours { get; set; }
}