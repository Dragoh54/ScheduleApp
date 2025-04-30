using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<RemoveParticipantFromMeetingCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(RemoveParticipantFromMeetingCommand request, CancellationToken cancellationToken)
    {
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
                 $"""
                  You were removed from meeting {participant.Meeting.Title}! 
                  No meeting for you fooly
                  """,
                 cancellationToken
             ));

        return participant.Adapt<ParticipantDto>();
    }
}