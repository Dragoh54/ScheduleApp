using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<UpdateMeetingStatusCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        meeting.Status = request.Status;
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        //TODO: THINK ABOUT NOTIFY ALL PARTICIPANTS THROUGH SIGNALR
        foreach (var participant in updatedMeeting.Participants)
        {
            BackgroundJob.Enqueue(() =>
                emailService.SendEmailAsync(
                    participant.Email,
                    $"Meeting status Updated",
                    $"""
                     Meeting {updatedMeeting.Title} was updated! 
                     Status: {updatedMeeting.Status}
                     """,
                    cancellationToken
                )); 
        }

        return updatedMeeting.Adapt<MeetingDto>();
    }
}