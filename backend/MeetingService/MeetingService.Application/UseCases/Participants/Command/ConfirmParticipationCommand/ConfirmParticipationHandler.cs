using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
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
    IEmailTokenService emailTokenService,
    IParticipantNotifier notifier
    ) : IRequestHandler<ConfirmParticipationCommand, ParticipantWithMeetingDto>
{
    public async Task<ParticipantWithMeetingDto> Handle(ConfirmParticipationCommand request, CancellationToken cancellationToken)
    {
        var success = await emailTokenService.CheckEmailToken(request.MeetingId, request.Email, request.Token, cancellationToken);
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
        
        var key = participantCacheService.CreateKey(request.MeetingId, request.Email);
        var participant = await participantCacheService.Get(key, cancellationToken)
            ?? throw new BadRequestException("Participant not found");

        await participantCacheService.Delete(key, cancellationToken);

        participant.Status = ParticipationStatus.Accepted;
        
        var participantInDatabase = await unitOfWork.ParticipantRepository.Add(participant, cancellationToken)
            ?? throw new BadRequestException("Participant could not be added");

        await unitOfWork.SaveChangesAsync();

        var meeting = await unitOfWork.MeetingRepository.GetById(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        cancellationToken.ThrowIfCancellationRequested();

        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(participant.Email,
                $"Welcome on board",
                $"You are subscribed to meeting {meeting.Title} at {meeting.StartTime:D} {meeting.StartTime:hh:mm}!",
                cancellationToken
            ));
        
        SendNotifications(meeting, participantInDatabase, cancellationToken);
        
        await notifier.NotifyJoinedAsync(meeting.Id, participant.UserId, meeting.Title!);
        
        return participantInDatabase.Adapt<ParticipantWithMeetingDto>();
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