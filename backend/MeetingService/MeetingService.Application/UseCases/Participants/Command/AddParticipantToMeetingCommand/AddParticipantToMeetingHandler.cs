using System.Web;
using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IEmailTokenService emailTokenService,
    IParticipantCacheService participantCacheService
    ) : IRequestHandler<AddParticipantToMeetingCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(AddParticipantToMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetMeetingWithParticipants(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");
        
        if (meeting.Participants.Any(p => p.UserId == request.UserId))
        {
            throw new AlreadyExistsException("User already on board!");
        }

        var participant = new Participant();
        request.Adapt(participant);

        var key = participantCacheService.CreateKey(participant.MeetingId, participant.Email);
        await participantCacheService.Set(participant, key, cancellationToken);
        
        cancellationToken.ThrowIfCancellationRequested();

        var confirmToken = await emailTokenService.GenerateEmailToken(meeting.Id, participant.Email, TokenTypes.ParticipantConfirmation, cancellationToken);
        
        var confirmLink = GenerateEmailTokenLink(request.CallbackUrl, participant.Email, confirmToken, ParticipationStatus.Accepted);
        var declineLink = GenerateEmailTokenLink(request.CallbackUrl, participant.Email, confirmToken, ParticipationStatus.Declined);

        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(
                participant.Email,
                "Meeting confirmation",
                $"""
                    <h1> Confirmation to {meeting.Title} </h1>
                    <p>Go thought this link to confirm:</p>
                    <a href="{confirmLink}">Confirm!</a>
                    <p>If you are not going to participate decline this or just ignore 24 hours.</p>
                    <a href="{declineLink}">Decline!</a>
                 """,
                cancellationToken
            ));
        
        //TODO: SEND NOTIFICATIONS THROUGH SIGNALR
        
        return participant.Adapt<ParticipantDto>();
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