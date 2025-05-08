using System.Globalization;
using Hangfire;
using MeetingService.Api.Helpers;
using MeetingService.Api.Hubs;
using MeetingService.Api.Interfaces;
using MeetingService.Api.Interfaces.Hubs;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Models;
using Microsoft.AspNetCore.SignalR;

namespace MeetingService.Api.Notifier;

public class MeetingNotifier(
    IHubContext<MeetingNotificationHub, IMeetingNotificationHub> hubContext,
    IUnitOfWork unitOfWork
    ) : IMeetingNotifier
{
    public async Task NotifyMeetingAsync(Guid meetingId, string meetingTitle, DateTime date, CancellationToken cancellationToken)
    {
        var stringId = meetingId.ToString();
        var stringDate = date.ToString(CultureInfo.InvariantCulture);
        
        var notifyTime = date.AddDays(-1);
        
        if (notifyTime <= DateTime.UtcNow)
        {
            await hubContext.Clients.Group(meetingId.ToString()).MeetingNotification(stringId, stringDate, meetingTitle);
        }
        else
        {
            var jobId = BackgroundJob.Schedule<MeetingHubHelper>(h =>
                    h.SendMeetingNotification(stringId, meetingTitle, stringDate),
                notifyTime);

            var scheduledJob = new ScheduledJob
            {
                JobId = jobId,
                MeetingId = meetingId,
                ExecuteAt = notifyTime
            };

            await unitOfWork.ScheduledJobRepository.Add(scheduledJob, cancellationToken);
        }
    }

    public async Task NotifyTimeChangedAsync(Guid meetingId, string meetingTitle, DateTime newStartTime)
    {
        await hubContext
            .Clients
            .Group(meetingId.ToString())
            .MeetingTimeChanged(meetingId.ToString(), meetingTitle, newStartTime.ToString(CultureInfo.InvariantCulture));
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
}