namespace MeetingService.Api.Interfaces.Notifiers;

public interface IParticipantNotifier
{
    Task NotifyInvitedAsync(Guid meetingId, Guid userId);
    Task NotifyJoinedAsync(Guid meetingId, Guid userId);
    Task NotifyRemovedAsync(Guid meetingId, Guid userId);
}