using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.NotificationDtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Handlers.Email;
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
    IEmailNotificationService emailNotificationService,
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
        
        var participant = await participantCacheService.GetParticipantFromCache(request.MeetingId, request.Email, cancellationToken);
        participant.Status = ParticipationStatus.Accepted;
        
        var participantInDatabase = await unitOfWork.ParticipantRepository.Add(participant, cancellationToken)
            ?? throw new BadRequestException("Participant could not be added");

        var meeting = participantInDatabase.Meeting;
        
        // var meeting = await unitOfWork.MeetingRepository.GetById(request.MeetingId, cancellationToken)
        //     ?? throw new NotFoundException("Meeting not found");
        
        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(participant.Email,
                $"Welcome on board",
                ParticipantEmailMessageHandler.ParticipationConfirmedBody(meeting.Title!, meeting.StartTime),
                cancellationToken
            ));
        
        await SendEmailNotification(meeting, participantInDatabase, cancellationToken);
        await notifier.NotifyJoinedAsync(meeting.Id, participantInDatabase.UserId, meeting.Title!);
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return participantInDatabase.Adapt<ParticipantWithMeetingDto>();
    }

    private bool ValidateParticipantStatus(ParticipationStatus participantStatus) => participantStatus switch
    {
        ParticipationStatus.Accepted => true,
        ParticipationStatus.Declined => false,
        _ => throw new BadRequestException("Invalid participation status")
    };

    private async Task SendEmailNotification(Meeting meeting, Participant participant, CancellationToken cancellationToken)
    {
        var notifyTime = meeting.NotifyTime;
        
        var emailNotificationDto = new EmailNotificationDto(participant.Email, meeting.Title!, meeting.StartTime);
        if (notifyTime > DateTime.UtcNow)
        {
            await emailNotificationService.SendNotificationAtNotifyTime(meeting.Id,  emailNotificationDto, notifyTime, cancellationToken);
        }
        else
        {
            await emailNotificationService.SendNotification(emailNotificationDto, cancellationToken);
        }
    }
}