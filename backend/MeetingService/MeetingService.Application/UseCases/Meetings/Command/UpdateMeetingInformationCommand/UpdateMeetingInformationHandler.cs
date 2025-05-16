using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public class UpdateMeetingInformationHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IMeetingNotifier notifier
    ) : IRequestHandler<UpdateMeetingInformationCommand, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(UpdateMeetingInformationCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var oldTitle = meeting.Title!;
        
        request.Adapt(meeting);
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
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
        
        await notifier.NotifyMeetingInformationChangedAsync(meeting.Id, oldTitle, newTitle);
        
        return updatedMeeting.Adapt<MeetingWithParticipantsDto>();
    }
    
    private Task SendEmailAsync(Participant participant, string oldTitle, string updatedTitle, string updatedDescription, CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                 emailService.SendEmailAsync(
                     participant.Email,
                     "Meeting information Updated",
                     MeetingEmailMessageHandler.MeetingInformationUpdatedBody(oldTitle, updatedTitle, updatedDescription),
                     ct
                 ));
        }, ct);
    }
}