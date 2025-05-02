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
    ITokenService tokenService,
    IEmailTokenProvider emailTokenProvider
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

        var participant = await unitOfWork.ParticipantRepository.Add(request.Adapt<Participant>(), cancellationToken)
            ?? throw new BadRequestException("Participant could not be added");
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();

        var confirmToken = await tokenService.GenerateEmailToken(meeting.Id, participant.Email, TokenTypes.ParticipantConfirmation, cancellationToken);
        var declineToken = await tokenService.GenerateEmailToken(meeting.Id, participant.Email, TokenTypes.ParticipantDeclination, cancellationToken);
        
        var confirmLink = GenerateEmailTokenLink(request.CallbackUrl, participant.Email, ParticipationStatus.Accepted, confirmToken);
        var declineLink = GenerateEmailTokenLink(request.CallbackUrl, participant.Email, ParticipationStatus.Declined, declineToken);

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
        
        //TODO: THINK ABOUT DELETION FROM AFK 24 HOURS
        
        //TODO: SEND NOTIFICATIONS THROUGH SIGNALR
        
        return participant.Adapt<ParticipantDto>();
    }
    
    private static string GenerateEmailTokenLink(string baseUrl, string email, ParticipationStatus status, string token)
    {
        var query = HttpUtility.ParseQueryString(string.Empty);

        query["email"] = email;
        query["token"] = HttpUtility.UrlEncode(token);
        query["status"] = ((int)status).ToString();
    
        var uriBuilder = new UriBuilder(baseUrl)
        {
            Query = query.ToString()
        };
    
        return uriBuilder.ToString();
    }
}