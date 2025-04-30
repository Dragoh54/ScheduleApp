using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
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
        
        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(
                participant.Email,
                $"Added to meeting",
                $"""
                 You were added to meeting {meeting.Title}! 
                 Meeting will be {meeting.StartTime:MM/dd/yyyy} at {meeting.StartTime:HH:mm}
                 """,
                cancellationToken
            )); 
        
        return participant.Adapt<ParticipantDto>();
    }
}