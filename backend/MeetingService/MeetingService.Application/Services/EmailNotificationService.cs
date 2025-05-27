using Hangfire;
using MeetingService.Application.Dtos.NotificationDto;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.Services;

public class EmailNotificationService : IEmailNotificationService
{
    public EmailNotificationService(IUnitOfWork unitOfWork, IEmailService emailService)
    {
        _emailService = emailService;
        _unitOfWork = unitOfWork;
    }
    
    private readonly IEmailService _emailService;
    private readonly IUnitOfWork _unitOfWork;

    public async Task SendNotificationAtNotifyTime(Guid meetingId, EmailNotificationDto dto, DateTime notifyTime,
        CancellationToken cancellationToken)
    {
        if (notifyTime < DateTime.UtcNow)
        {
            await SendNotification(dto, cancellationToken);
            return;
        }
        
        var jobId = BackgroundJob.Schedule(() =>
            _emailService.SendEmailAsync(
                dto.ParticipantEmail,
                $"Reminder",
                $"Meeting {dto.MeetingTitle} will be at {dto.StartTime:f}!",
                cancellationToken
            ), notifyTime);

        var scheduleJob = new ScheduledJob(meetingId, jobId, notifyTime);

        await _unitOfWork.ScheduledJobRepository.Add(scheduleJob, cancellationToken);

        await _unitOfWork.SaveChangesAsync();
    }

    public async Task SendNotification(EmailNotificationDto dto, CancellationToken cancellationToken)
    {
        await _emailService.SendEmailAsync(
            dto.ParticipantEmail,
            $"Reminder",
            $"Meeting {dto.MeetingTitle} will be at {dto.StartTime:hh:mm}!",
            cancellationToken
        );
    }
}