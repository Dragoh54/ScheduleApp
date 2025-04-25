using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Command.UpdateParticipantStatusCommand;

public record UpdateParticipantStatusCommand : IRequest<ParticipantDto>
{
    
}