using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public record GetParticipantQuery : IRequest<ParticipantDto>
{
    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
}