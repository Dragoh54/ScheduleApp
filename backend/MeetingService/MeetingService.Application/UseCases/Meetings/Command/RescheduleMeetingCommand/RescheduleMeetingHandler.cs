using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public class RescheduleMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IScheduledJobsService scheduledJobsService,
    IMeetingNotifier notifier
    ) : IRequestHandler<RescheduleMeetingCommand, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(RescheduleMeetingCommand request, CancellationToken cancellationToken)
    {
        var notifyTime = request.NotifyTime ?? request.StartTime.AddDays(-1);
        if (notifyTime < DateTime.Now)
        {
            throw new BadRequestException("Notify time cannot be in past");
        }
        
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        request.Adapt(meeting);
        request.NotifyTime = notifyTime;
        
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
        
        await notifier.NotifyTimeChangedAsync(meeting.Id, meetingTitle, newStartTime, notifyTime, cancellationToken);
        
        return updatedMeeting.Adapt<MeetingWithParticipantsDto>();
    }

    private Task SendEmailAsync(Participant participant, string oldTitle, DateTime newStartTime, DateTime newEndTime, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                emailService.SendEmailAsync(
                    participant.Email,
                    "Meeting Rescheduled",
                    $"""
                     Meeting {oldTitle} was rescheduled! 
                     Start time: {newStartTime},
                     End time: {newEndTime}
                     """,
                    ct
                ));
        }, ct);
    }
}