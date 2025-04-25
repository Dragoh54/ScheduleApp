using MediatR;
using MeetingService.Application.Dtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public record GetParticipantQuery : IRequest<ParticipantDto>
{
    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
}