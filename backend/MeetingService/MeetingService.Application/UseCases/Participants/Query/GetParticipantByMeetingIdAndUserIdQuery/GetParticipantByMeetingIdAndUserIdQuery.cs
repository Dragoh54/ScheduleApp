using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByMeetingIdAndUserIdQuery;

public record GetParticipantByMeetingIdAndUserIdQuery : IRequest<ParticipantDto>
{
    
}