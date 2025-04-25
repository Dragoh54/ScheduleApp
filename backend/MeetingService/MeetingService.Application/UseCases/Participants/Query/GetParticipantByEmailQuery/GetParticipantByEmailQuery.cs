using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public record GetParticipantByEmailQuery : IRequest<ParticipantDto>
{
    
}