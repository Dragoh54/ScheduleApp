using System.Security.Authentication;
using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Providers;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DataAccess.Repositories;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IJwtProvider jwtProvider
    ) : IRequestHandler<RemoveParticipantFromMeetingCommand, ParticipantWithMeetingDto>
{
    public async Task<ParticipantWithMeetingDto> Handle(RemoveParticipantFromMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetById(request.MeetingId, cancellationToken)
                      ?? throw new NotFoundException("Meeting not found");
        
        var idFromAccessToken = await jwtProvider.GetUserIdFromToken(request.AccessToken);
        
        var isUserValid = idFromAccessToken == request.UserId || idFromAccessToken == meeting.OrganizationUserId;
        if (!isUserValid)
        {
            throw new AuthenticationException("You are not able to perform this action");
        }
        
        var participant = await unitOfWork.ParticipantRepository.GetParticipantWithMeeting(request.MeetingId, request.UserId, cancellationToken)
                      ?? throw new NotFoundException("Participant not found");
        
        var success = await unitOfWork.ParticipantRepository.Delete(participant, cancellationToken);
        if (!success)
        {
            throw new BadRequestException("Failed to delete participant");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
         BackgroundJob.Enqueue(() =>
             emailService.SendEmailAsync(
                 participant.Email,
                 $"Removed from meeting",
                 $"You were removed from meeting {participant.Meeting.Title}!",
                 cancellationToken
             ));

        return participant.Adapt<ParticipantWithMeetingDto>();
    }
}