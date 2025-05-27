using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Api.Interfaces.Notifiers;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Notifier;

public class ParticipantNotifier : IParticipantNotifier
{
    public ParticipantNotifier(
        IUserConnectionManager userConnectionManager,
        IHubContext<ParticipantNotificationHub, IParticipantNotificationHub> hubContext
    )
    {
        _userConnectionManager = userConnectionManager;
        _hubContext = hubContext;
    }

    private readonly IUserConnectionManager _userConnectionManager;
    private readonly IHubContext<ParticipantNotificationHub, IParticipantNotificationHub> _hubContext;
    
    public async Task NotifyInvitedAsync(Guid meetingId, Guid userId, string meetingTitle)
    {
        await NotifyAsync(userId, client =>
            client.InvitedToMeeting(meetingId.ToString(), meetingTitle));
    }

    public async Task NotifyJoinedAsync(Guid meetingId, Guid userId, string meetingTitle)
    {
        await NotifyAsync(userId, client =>
            client.JoinedToMeeting(meetingId.ToString(), meetingTitle));
    }

    public async Task NotifyRemovedAsync(Guid meetingId, Guid userId, string meetingTitle)
    {
        await NotifyAsync(userId, client =>
            client.RemovedFromMeeting(meetingId.ToString(), meetingTitle));
    }
    private async Task NotifyAsync(Guid userId, Func<IParticipantNotificationHub, Task> notifyAction)
    {
        var connections = _userConnectionManager.GetUserConnections(userId.ToString());

        foreach (var connectionId in connections)
        {
            var client = _hubContext.Clients.Client(connectionId);
            await notifyAction(client);
        } 
    }
}