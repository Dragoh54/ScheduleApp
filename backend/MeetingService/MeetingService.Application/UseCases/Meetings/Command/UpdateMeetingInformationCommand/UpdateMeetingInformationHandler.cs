using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingInformationCommand;

public class UpdateMeetingInformationHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<UpdateMeetingInformationCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingInformationCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        var oldTitle = meeting.Title;

        request.Adapt(meeting);
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        //TODO: THINK ABOUT NOTIFY THROUGH SIGNALR
        foreach (var participant in updatedMeeting.Participants)
        {
            BackgroundJob.Enqueue(() =>
                emailService.SendEmailAsync(
                    participant.Email,
                    "Meeting information Updated",
                    $"""
                     Meeting {oldTitle} was updated! 
                     Title: {updatedMeeting.Title},
                     Description: {updatedMeeting.Description}
                     """,
                    cancellationToken
                ));
        }
        
        return updatedMeeting.Adapt<MeetingDto>();
    }
}