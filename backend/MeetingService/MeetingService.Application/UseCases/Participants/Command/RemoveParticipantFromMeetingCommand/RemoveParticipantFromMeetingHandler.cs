using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;

namespace MeetingService.Application.UseCases.Participants.Command.RemoveParticipantFromMeetingCommand;

public class RemoveParticipantFromMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<RemoveParticipantFromMeetingCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(RemoveParticipantFromMeetingCommand request, CancellationToken cancellationToken)
    {
        var participant = await unitOfWork.ParticipantRepository.GetById(request.Id, cancellationToken)
                      ?? throw new NotFoundException("Participant not found");
        
        var success = await unitOfWork.ParticipantRepository.Delete(participant, cancellationToken);

        if (!success)
        {
            throw new BadRequestException("Failed to delete participant");
        }
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();

        return participant.Adapt<ParticipantDto>();
    }
}