using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public class ConfirmParticipationHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IParticipantCacheService participantCacheService,
    IEmailTokenService emailTokenService
    ) : IRequestHandler<ConfirmParticipationCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(ConfirmParticipationCommand request, CancellationToken cancellationToken)
    {
        //TODO: CHECK TOKENS
        var success =
            await emailTokenService.CheckEmailToken(request.MeetingId, request.Email, request.Token, cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Invalid token");
        }

        var statusFromQuery = (ParticipationStatus)Enum.Parse(typeof(ParticipationStatus), request.ParticipationStatusString);
        
        var isStatusAccepted = ValidateParticipantStatus(statusFromQuery);

        if (!isStatusAccepted)
        {
            throw new BadRequestException("User declined participation");
        }
        
        //TODO: GET FROM CACHE PARTICIPANT
        
        //TODO: ADD TO DATABASE PARTICIPANT
        
        //TODO: SEND EMAIL TO PARTICIPANT
        
        throw new NotImplementedException();
    }
    
    private void SendNotifications(Meeting meeting, Participant participant, CancellationToken cancellationToken)
    {
        var notifyBeforeDay = meeting.StartTime.AddDays(-1);
        var notifyBeforeHour = meeting.StartTime.AddHours(-1);
        
        BackgroundJob.Schedule(() => 
            emailService.SendEmailAsync(
                participant.Email,
                $"Reminder",
                $"Meeting {meeting.Title} will be next day at {meeting.StartTime:hh:mm}!",
                cancellationToken
            ), notifyBeforeDay);
        
        BackgroundJob.Schedule(() => 
            emailService.SendEmailAsync(
                participant.Email,
                $"Reminder",
                $"Meeting {meeting.Title} starts soon: {meeting.StartTime:hh:mm}!",
                cancellationToken
            ), notifyBeforeHour);
    }

    private bool ValidateParticipantStatus(ParticipationStatus participantStatus) => participantStatus switch
    {
        ParticipationStatus.Accepted => true,
        ParticipationStatus.Declined => false,
        _ => throw new BadRequestException("Invalid participation status")
    };
}