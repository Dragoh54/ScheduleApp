namespace MeetingService.Application.Handlers.Email;

public static class ParticipantEmailMessageHandler
{
    public static string MeetingConfirmationBody(string meetingTitle, string confirmLink, string declineLink) =>
        $"""
            <h1> Confirmation to {meetingTitle} </h1>
            <p>Go thought this link to confirm:</p>
            <a href="{confirmLink}">Confirm!</a>
            <p>If you are not going to participate decline this or just ignore 24 hours.</p>
            <a href="{declineLink}">Decline!</a>
         """;

    public static string ParticipationConfirmedBody(string meetingTitle, DateTime startTime) =>
        $"You are subscribed to meeting {meetingTitle} on {startTime:f}!";

    public static string RemoveParticipantBody(string meetingTitle) =>
        $"You were removed from meeting {meetingTitle}!";
}