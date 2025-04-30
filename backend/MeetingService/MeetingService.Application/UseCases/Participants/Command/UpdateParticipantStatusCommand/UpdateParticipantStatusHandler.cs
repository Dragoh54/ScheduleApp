using Hangfire;
using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.Application.Interfaces.Services;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public class UpdateParticipantStatusHandler(
    IUnitOfWork unitOfWork,
    IEmailService emailService
    ) : IRequestHandler<UpdateParticipantStatusCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(UpdateParticipantStatusCommand request, CancellationToken cancellationToken)
    {
        var participant = await unitOfWork.ParticipantRepository.GetParticipantWithMeeting(request.MeetingId, request.UserId, cancellationToken)
                          ?? throw new NullReferenceException("Participant not found");
        
        //TODO: THINK ABOUT DELETING IN THIS HANDLER
        if (request.Status == ParticipationStatus.Declined)
        {
            await DeleteParticipant(participant, cancellationToken);
        
            return participant.Adapt<ParticipantDto>();
        }
        
        participant.Status = request.Status;
        
        var updatedParticipant = await unitOfWork.ParticipantRepository.Update(participant, cancellationToken)
            ?? throw new BadRequestException("Participant not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        BackgroundJob.Enqueue(() =>
            emailService.SendEmailAsync(
                participant.Email,
                $"Your status of participating updated",
                $"Your status for meeting {participant.Meeting.Title} is {participant.Status}!",
                cancellationToken
            ));
        
        return updatedParticipant.Adapt<ParticipantDto>();
    }

    private async Task DeleteParticipant(Participant participant, CancellationToken cancellationToken)
    {
        var success = await unitOfWork.ParticipantRepository.Delete(participant, cancellationToken);
        
        if (!success)
        {
            throw new BadRequestException("Failed to delete declined participant");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
    }
}