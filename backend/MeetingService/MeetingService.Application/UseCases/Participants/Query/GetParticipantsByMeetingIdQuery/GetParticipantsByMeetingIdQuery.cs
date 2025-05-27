using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Responses;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantsByMeetingIdQuery;

public record GetParticipantsByMeetingIdQuery : IRequest<IEnumerable<ParticipantWithMeetingResponseDto>>
{
    public Guid MeetingId { get; set; }
}