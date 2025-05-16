using Hangfire;
using MeetingService.Application.Dtos.NotificationDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Services;

public class EmailNotificationService(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IEmailNotificationService
{
    public async Task SendNotificationAtNotifyTime(Guid meetingId, EmailNotificationDto dto, DateTime notifyTime,
        CancellationToken cancellationToken)
    {
        if (notifyTime < DateTime.UtcNow)
        {
            await SendNotification(dto, cancellationToken);
            return;
        }
        
        var jobId = BackgroundJob.Schedule(() =>
            emailService.SendEmailAsync(
                dto.ParticipantEmail,
                $"Reminder",
                $"Meeting {dto.MeetingTitle} will be at {dto.StartTime:f}!",
                cancellationToken
            ), notifyTime);

        var scheduleJob = new ScheduledJob(meetingId, jobId, notifyTime);

        await unitOfWork.ScheduledJobRepository.Add(scheduleJob, cancellationToken);

        await unitOfWork.SaveChangesAsync();
    }

    public async Task SendNotification(EmailNotificationDto dto, CancellationToken cancellationToken)
    {
        await emailService.SendEmailAsync(
            dto.ParticipantEmail,
            $"Reminder",
            $"Meeting {dto.MeetingTitle} will be at {dto.StartTime:hh:mm}!",
            cancellationToken
        );
    }
}