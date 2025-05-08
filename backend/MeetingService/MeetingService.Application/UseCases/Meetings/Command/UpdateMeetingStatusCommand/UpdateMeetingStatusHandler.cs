using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Meetings.Command.UpdateMeetingStatusCommand;

public class UpdateMeetingStatusHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<UpdateMeetingStatusCommand, MeetingWithParticipantsDto>
{
    public async Task<MeetingWithParticipantsDto> Handle(UpdateMeetingStatusCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        meeting.Status = request.Status;
        
        var updatedMeeting = await unitOfWork.MeetingRepository.Update(meeting, cancellationToken)
            ?? throw new BadRequestException("Meeting not updated");

        await unitOfWork.SaveChangesAsync();
        
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

        return updatedMeeting.Adapt<MeetingWithParticipantsDto>();
    }
    
    private Task SendEmailAsync(Participant participant, string meetingTitle, MeetingStatus updatedMeetingStatus,  CancellationToken ct)
    {
        return Task.Run(() =>
        {
            BackgroundJob.Enqueue(() =>
                emailService.SendEmailAsync(
                    participant.Email,
                    $"Meeting status Updated",
                    $"""
                     Meeting {meetingTitle} was updated! 
                     Status: {updatedMeetingStatus}
                     """,
                    ct
                )); 
        }, ct);
    }
}