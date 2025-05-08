using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces.Helpers;
using MeetingService.Api.Interfaces.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Helpers;

public class MeetingHubHelper : IMeetingHubHelper
{
    private readonly IHubContext<MeetingNotificationHub, IMeetingNotificationHub> _hubContext;

    public MeetingHubHelper(IHubContext<MeetingNotificationHub, IMeetingNotificationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task SendMeetingNotification(string meetingId, string meetingTitle, string date)
    {
        await _hubContext.Clients.Group(meetingId).MeetingNotification(meetingId, meetingTitle, date);
    }
}