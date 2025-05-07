using System.Security.Claims;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Application.Dtos.MeetingDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Hubs;

public class MeetingNotificationHub : Hub<IMeetingNotificationHub>
{
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}