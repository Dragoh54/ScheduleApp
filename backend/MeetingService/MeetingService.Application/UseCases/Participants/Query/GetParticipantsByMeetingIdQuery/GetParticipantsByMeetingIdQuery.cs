using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public record GetParticipantsByMeetingIdQuery : IRequest<IEnumerable<ParticipantWithMeetingDto>>
{
    public Guid MeetingId { get; set; }
}