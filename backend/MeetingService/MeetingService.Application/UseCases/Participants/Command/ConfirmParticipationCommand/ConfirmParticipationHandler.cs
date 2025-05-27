using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.NotificationDtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.ConfirmParticipationCommand;

public class ConfirmParticipationHandler : IRequestHandler<ConfirmParticipationCommand, ParticipantWithMeetingDto>
{
    public ConfirmParticipationHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IParticipantCacheService participantCacheService,
        IEmailTokenService emailTokenService,
        IEmailNotificationService emailNotificationService,
        IParticipantNotifier participantNotifier,
        IMeetingNotifier meetingNotifier
    )
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _participantCacheService = participantCacheService;
        _emailTokenService = emailTokenService;
        _emailNotificationService = emailNotificationService;
        _participantNotifier = participantNotifier;
        _meetingNotifier = meetingNotifier;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IParticipantCacheService _participantCacheService;
    private readonly IEmailTokenService _emailTokenService;
    private readonly IEmailNotificationService _emailNotificationService;
    private readonly IParticipantNotifier _participantNotifier;
    private readonly IMeetingNotifier _meetingNotifier;
    
    public async Task<ParticipantWithMeetingDto> Handle(ConfirmParticipationCommand request, CancellationToken cancellationToken)
    {
        var success = await _emailTokenService.CheckEmailToken(request.MeetingId, request.Email, request.Token, cancellationToken);
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
        
        var meeting = await _unitOfWork.MeetingRepository.GetById(request.MeetingId, cancellationToken)
                      ?? throw new BadRequestException("Meeting could not be found");
        
        var isAcceptableMeeting = meeting.Status is MeetingStatus.Completed or MeetingStatus.Cancelled ||
                                  meeting.StartTime < DateTime.Now;
        if (isAcceptableMeeting)
        {
            throw new BadRequestException("Meeting is already completed or cancelled");
        }
        
        var participant = await _participantCacheService.GetParticipantFromCache(request.MeetingId, request.Email, cancellationToken);
        participant.Status = ParticipationStatus.Accepted;
        
        var participantInDatabase = await _unitOfWork.ParticipantRepository.Add(participant, cancellationToken)
            ?? throw new BadRequestException("Participant could not be added");
        
        BackgroundJob.Enqueue(() =>
            _emailService.SendEmailAsync(participant.Email,
                $"Welcome on board",
                ParticipantEmailMessageHandler.ParticipationConfirmedBody(meeting.Title!, meeting.StartTime),
                cancellationToken
            ));
        
        await SendEmailNotification(meeting, participantInDatabase, cancellationToken);
        await _participantNotifier.NotifyJoinedAsync(meeting.Id, participantInDatabase.UserId, meeting.Title!);
        await _meetingNotifier.NotifyOnTimeAsync(meeting.Id, meeting.Title!, meeting.StartTime, meeting.NotifyTime, cancellationToken);
        
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
        if (notifyTime < DateTime.UtcNow)
        {
            await _emailNotificationService.SendNotification(emailNotificationDto, cancellationToken);
        }
        else
        {
            await _emailNotificationService.SendNotificationAtNotifyTime(meeting.Id,  emailNotificationDto, notifyTime, cancellationToken);
        }
    }
}