using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public record GetParticipantsByMeetingIdQuery : IRequest<IEnumerable<ParticipantDto>>
{
    public Guid MeetingId { get; set; }
}