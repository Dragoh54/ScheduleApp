using Mapster;
using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;
using MeetingService.DataAccess.Interfaces.UnitOfWork;
using MeetingService.DomainModel.Exceptions;
using MeetingService.DomainModel.Models;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public class AddParticipantToMeetingHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddParticipantToMeetingCommand, ParticipantDto>
{
    public async Task<ParticipantDto> Handle(AddParticipantToMeetingCommand request, CancellationToken cancellationToken)
    {
        var meeting = await unitOfWork.MeetingRepository.GetById(request.MeetingId, cancellationToken)
            ?? throw new NotFoundException("Meeting not found");

        var participant = await unitOfWork.ParticipantRepository.Add(request.Adapt<Participant>(), cancellationToken)
            ?? throw new BadRequestException("Participant could not be added");
        
        await unitOfWork.SaveChangesAsync();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        return participant.Adapt<ParticipantDto>();
    }
}