using System.Security.Claims;
using MeetingService.Api.Interfaces.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Hubs;

public class MeetingNotificationHub : Hub<IMeetingNotificationHub>
{
    public async Task JoinMeetingGroup(string meetingId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, meetingId);
    }
    
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}