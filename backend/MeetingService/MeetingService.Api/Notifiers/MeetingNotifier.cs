using System.Globalization;
using Hangfire;
using MeetingService.Api.Helpers;
using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Notifiers;

public class MeetingNotifier(
    IHubContext<MeetingNotificationHub, IMeetingNotificationHub> hubContext,
    IUnitOfWork unitOfWork
    ) : IMeetingNotifier
{
    public async Task NotifyOnTimeAsync(Guid meetingId, string meetingTitle, DateTime newStartTime, DateTime notifyTime, CancellationToken cancellationToken)
    {
        var stringDate = newStartTime.ToString(CultureInfo.InvariantCulture);
        
        if (notifyTime <= DateTime.UtcNow)
        {
            await hubContext.Clients.Group(meetingId.ToString()).MeetingNotification(meetingId.ToString(), stringDate, meetingTitle);
        }
        else
        {
            await SetScheduledJob(meetingId, meetingTitle, stringDate, notifyTime, cancellationToken);
        }
    }

    public async Task NotifyMeetingDeletedAsync(Guid meetingId, string meetingTitle)
    {
        await hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingDeleted(meetingId.ToString(), meetingTitle);
    }

    public async Task NotifyMeetingInformationChangedAsync(Guid meetingId, string oldTitle, string newTitle)
    {
        await hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingInformationUpdated(meetingId.ToString(), oldTitle, newTitle);
    }

    public async Task NotifyMeetingStatusChangedAsync(Guid meetingId, string meetingTitle, MeetingStatus newStatus)
    {
        await hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingStatusChanged(meetingId.ToString(), meetingTitle, newStatus.ToString());
    }

    private async Task SetScheduledJob(Guid meetingId, string meetingTitle, string date, DateTime notifyTime, CancellationToken cancellationToken)
    {
        var jobId = BackgroundJob.Schedule<MeetingHubHelper>(h =>
                h.SendMeetingNotification(meetingId.ToString(), meetingTitle, date), 
            notifyTime);

        var scheduledJob = new ScheduledJob(meetingId, jobId, notifyTime);

        await unitOfWork.ScheduledJobRepository.Add(scheduledJob, cancellationToken);

        await unitOfWork.SaveChangesAsync();
    } 
}