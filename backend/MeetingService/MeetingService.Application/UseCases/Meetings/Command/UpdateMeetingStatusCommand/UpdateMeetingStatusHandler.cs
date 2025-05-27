using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler : IRequestHandler<UpdateMeetingStatusCommand, MeetingWithParticipantsDto>
{
    public UpdateMeetingStatusHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IMeetingNotifier notifier
    )
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _notifier = notifier;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IMeetingNotifier _notifier;
    public async Task<MeetingWithParticipantsDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        meeting.Status = request.Status;
        
        var updatedMeeting = await _unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();

        var meetingTitle = updatedMeeting.Title!;
        var updatedMeetingStatus = updatedMeeting.Status;
        
        await Parallel.ForEachAsync(
            updatedMeeting.Participants,
            cancellationToken,
            async (participant, ct) =>
            {
                await SendEmailAsync(participant, meetingTitle, updatedMeetingStatus, ct);
            });
        
        await _notifier.NotifyMeetingStatusChangedAsync(meeting.Id, meetingTitle, updatedMeetingStatus);

        return updatedMeeting.Adapt<MeetingWithParticipantsDto>();
    }
    
    private Task SendEmailAsync(Participant participant, string meetingTitle, MeetingStatus updatedMeetingStatus,  CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                _emailService.SendEmailAsync(
                    participant.Email,
                    $"Meeting status Updated",
                    MeetingEmailMessageHandler.MeetingStatusUpdatedBody(meetingTitle, updatedMeetingStatus),
                    ct
                )); 
        }, ct);
    }
}