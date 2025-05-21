using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Dtos.NotificationDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public class RescheduleMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IScheduledJobsService scheduledJobsService,
    IMeetingNotifier notifier,
    IEmailNotificationService emailNotificationService
    ) : IRequestHandler<RescheduleMeetingCommand, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(RescheduleMeetingCommand request, CancellationToken cancellationToken)
    {
        if (request.NotifyTime < DateTime.Now)
        {
            throw new BadRequestException("Notify time cannot be in past");
        }
        
        var notifyTime = request.NotifyTime ?? request.StartTime.AddDays(-1);
        
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        meeting.Adapt(request);
        meeting.NotifyTime = notifyTime;
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var meetingTitle = meeting.Title!;
        var newStartTime = updatedMeeting.StartTime;
        var newEndTime = updatedMeeting.EndTime;
        
        await Parallel.ForEachAsync(
            updatedMeeting.Participants,
            cancellationToken,
            async (participant, ct) =>
                await SendEmailAsync(participant, meetingTitle, newStartTime, newEndTime, ct));
        
        await scheduledJobsService.DeleteScheduledJobs(meeting.Id, cancellationToken);
        
        await notifier.NotifyOnTimeAsync(meeting.Id, meetingTitle, newStartTime, notifyTime, cancellationToken);
        await SendEmailNotificationAsync(meeting, cancellationToken);
        
        return updatedMeeting.Adapt<MeetingWithParticipantsDto>();
    }

    private Task SendEmailAsync(Participant participant, string meetingTitle, DateTime newStartTime, DateTime newEndTime, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                emailService.SendEmailAsync(
                    participant.Email,
                    "Meeting Rescheduled",
                    MeetingEmailMessageHandler.MeetingRescheduledBody(meetingTitle, newStartTime, newEndTime),
                    ct
                ));
        }, ct);
    }

    private async Task SendEmailNotificationAsync(Meeting meeting, CancellationToken cancellationToken)
    {
        await Parallel.ForEachAsync(
            meeting.Participants,
            cancellationToken,
            async (participant, ct) =>
            {
                var emailNotificationDto = new EmailNotificationDto(participant.Email, meeting.Title!, meeting.StartTime);
                await emailNotificationService.SendNotificationAtNotifyTime(meeting.Id,  emailNotificationDto, meeting.NotifyTime, ct);
            });
    }
}