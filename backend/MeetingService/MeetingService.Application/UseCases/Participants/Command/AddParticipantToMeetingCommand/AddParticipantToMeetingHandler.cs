using System.Globalization;
using System.Security.Authentication;
using System.Web;
using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Api.Interfaces.Notifiers;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Handlers.Email;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.Application.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingHandler(
    IUnitOfWork unitOfWork,
    IJwtProvider jwtProvider,
    IEmailService emailService,
    IEmailTokenService emailTokenService,
    IParticipantCacheService participantCacheService,
    IParticipantNotifier notifier
    ) : IRequestHandler<AddParticipantToMeetingCommand, ParticipantWithMeetingDto>
{
    public async Task<ParticipantWithMeetingDto> Handle(AddParticipantToMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        var isAcceptableMeeting = meeting.Status is MeetingStatus.Completed or MeetingStatus.Cancelled ||
                                  meeting.StartTime < DateTime.Now;
        if (isAcceptableMeeting)
        {
            throw new BadRequestException("Meeting is already completed or cancelled");
        }
        
        var idFromAccessToken = await jwtProvider.GetUserIdFromToken(request.AccessToken);
        
        var isUserValid = idFromAccessToken == meeting.OrganizationUserId;
        if (!isUserValid)
        {
            throw new AuthenticationException("Only organization admin can add participants");
        }
        
        if (meeting.Participants.Any(p => p.UserId == request.UserId))
        {
            throw new AlreadyExistsException("User already on board!");
        }
        
        var participant = request.Adapt<Participant>();
        await participantCacheService.AddParticipantToCacheAsync(participant, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        var confirmToken = await emailTokenService.GenerateEmailToken(meeting.Id, participant.Email, TokenTypes.ParticipantConfirmation, cancellationToken);
        
        var confirmLink = GenerateEmailTokenLink(request.CallbackUrl, participant.Email, confirmToken, ParticipationStatus.Accepted);
        var declineLink = GenerateEmailTokenLink(request.CallbackUrl, participant.Email, confirmToken, ParticipationStatus.Declined);

        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(
                participant.Email,
                "Meeting confirmation",
                ParticipantEmailMessageHandler.MeetingConfirmationBody(meeting.Title!, confirmLink, declineLink),
                cancellationToken
            ));
        
        await notifier.NotifyInvitedAsync(meeting.Id, participant.UserId, meeting.Title!);
        
        return participant.Adapt<ParticipantWithMeetingDto>();
    }
    
    private static string GenerateEmailTokenLink(string baseUrl, string email, string token, ParticipationStatus participationStatus)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["email"] = email;
        query["token"] = HttpUtility.UrlEncode(token);
        query["participation_status"] = participationStatus.ToString();
    
        var uriBuilder = new UriBuilder(baseUrl)
        {
            Query = query.ToString()
        };
    
        return uriBuilder.ToString();
    }
}