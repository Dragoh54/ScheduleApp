namespace MeetingService.Application.Dtos.NotificationDtos;

public record EmailNotificationDto
{
    public EmailNotificationDto()
    {
    }

    public EmailNotificationDto(string participantEmail, string meetingTitle, DateTime startTime)
    {
        ParticipantEmail = participantEmail;
        MeetingTitle = meetingTitle;
        StartTime = startTime;
    }

    public string ParticipantEmail { get; init; } = string.Empty;
    public string MeetingTitle { get; init; } = string.Empty;
    public DateTime StartTime { get; init; }
}