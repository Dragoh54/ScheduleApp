using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public class UpdateMeetingInformationHandler : IRequestHandler<UpdateMeetingInformationCommand, MeetingWithParticipantsDto>
{
    public UpdateMeetingInformationHandler(
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
    
    public async Task<MeetingWithParticipantsDto> Handle(UpdateMeetingInformationCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var oldTitle = meeting.Title!;
        
        request.Adapt(meeting);
        
        var updatedMeeting = await _unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var newTitle = updatedMeeting.Title!;
        var newDescription = updatedMeeting.Description!;
        
        await Parallel.ForEachAsync(
            updatedMeeting.Participants,
            cancellationToken,
            async (participant, ct) =>
            {
                await SendEmailAsync(participant, oldTitle, newTitle, newDescription, ct);
            });
        
        await _notifier.NotifyMeetingInformationChangedAsync(meeting.Id, oldTitle, newTitle);
        
        return updatedMeeting.Adapt<MeetingWithParticipantsDto>();
    }
    
    private Task SendEmailAsync(Participant participant, string oldTitle, string updatedTitle, string updatedDescription, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                _emailService.SendEmailAsync(
                     participant.Email,
                     "Meeting information Updated",
                     MeetingEmailMessageHandler.MeetingInformationUpdatedBody(oldTitle, updatedTitle, updatedDescription),
                     ct
                 ));
        }, ct);
    }
}