using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Meetings.Command.RescheduleMeetingCommand;

public class RescheduleMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<RescheduleMeetingCommand, MeetingDto>
{
    public async Task<MeetingDto> Handle(RescheduleMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.Id, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        request.Adapt(meeting);
        
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
                    "Meeting Rescheduled",
                    $"""
                     Meeting {meeting.Title} was rescheduled! 
                     Start time: {updatedMeeting.StartTime},
                     End time: {updatedMeeting.EndTime}
                     """,
                    cancellationToken
                ));
        }
        
        return updatedMeeting.Adapt<MeetingDto>();
    }
}