using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Enums;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public class UpdateParticipantStatusHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateParticipantStatusCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(UpdateParticipantStatusCommand request, CancellationToken cancellationToken)
    {
        var participant = await unitOfWork.ParticipantRepository.GetParticipant(request.MeetingId, request.Id, cancellationToken)
                          ?? throw new NullReferenceException("Participant not found");
        
        //TODO: THINK ABOUT DELETING IN THIS HANDLER
        // if (request.Status == ParticipationStatus.Declined)
        // {
        //     var success = await unitOfWork.ParticipantRepository.Delete(participant, cancellationToken);
        //
        //     if (!success)
        //     {
        //         throw new BadRequestException("Failed to delete declined participant");
        //     }
        //
        //     await unitOfWork.SaveChangesAsync();
        //
        //     cancellationToken.ThrowIfCancellationRequested();
        //
        //     return participant.Adapt<ParticipantDto>();
        // }
        
        participant.Status = request.Status;
        
        var updatedParticipant = await unitOfWork.ParticipantRepository.Update(participant, cancellationToken)
            ?? throw new BadRequestException("Participant not updated");

        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return updatedParticipant.Adapt<ParticipantDto>();
    }
}