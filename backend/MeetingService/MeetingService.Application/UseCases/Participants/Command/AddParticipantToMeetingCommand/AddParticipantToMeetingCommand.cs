using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Command.AddParticipantToMeetingCommand;

public record AddParticipantToMeetingCommand : IRequest<ParticipantDto>
{
    
}