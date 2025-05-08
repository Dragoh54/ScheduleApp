using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Interfaces.Notifiers;

public interface IParticipantNotifier
{
    Task NotifyInvitedAsync(Guid meetingId, Guid userId, string meetingTitle);
    Task NotifyJoinedAsync(Guid meetingId, Guid userId, string meetingTitle);
    Task NotifyRemovedAsync(Guid meetingId, Guid userId, string meetingTitle);
}