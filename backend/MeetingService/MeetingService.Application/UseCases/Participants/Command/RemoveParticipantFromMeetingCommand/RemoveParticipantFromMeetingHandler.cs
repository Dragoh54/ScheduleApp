using System.Security.Authentication;
using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingHandler : IRequestHandler<RemoveParticipantFromMeetingCommand, ParticipantWithMeetingDto>
{
    public RemoveParticipantFromMeetingHandler(
        IUnitOfWork unitOfWork,
        IEmailService emailService,
        IJwtProvider jwtProvider,
        IParticipantNotifier notifier
    )
    {
        _unitOfWork = unitOfWork;
        _emailService = emailService;
        _jwtProvider = jwtProvider;
        _notifier = notifier;
    }

    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailService _emailService;
    private readonly IJwtProvider _jwtProvider;
    private readonly IParticipantNotifier _notifier;
    
    public async Task<ParticipantWithMeetingDto> Handle(RemoveParticipantFromMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await _unitOfWork.MeetingRepository.GetById(request.MeetingId, cancellationToken)
                      ?? throw new NotFoundException("Meeting not found");
        
        var idFromAccessToken = await _jwtProvider.GetUserIdFromToken(request.AccessToken);
        
        var isUserValid = idFromAccessToken == request.UserId || idFromAccessToken == meeting.OrganizationUserId;
        if (!isUserValid)
        {
            throw new AuthenticationException("You are not able to perform this action");
        }
        
        var participant = await _unitOfWork.ParticipantRepository.GetParticipantWithMeeting(request.MeetingId, request.UserId, cancellationToken)
                      ?? throw new NotFoundException("Participant not found");
        
        var success = await _unitOfWork.ParticipantRepository.Delete(participant, cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to delete participant");
        }
        
        await _unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
         BackgroundJob.Enqueue(() =>
             _emailService.SendEmailAsync(
                 participant.Email,
                 $"Removed from meeting",
                 ParticipantEmailMessageHandler.RemoveParticipantBody(meeting.Title!),
                 cancellationToken
             ));
         
         await _notifier.NotifyRemovedAsync(meeting.Id, participant.UserId, meeting.Title!);

        return participant.Adapt<ParticipantWithMeetingDto>();
    }
}