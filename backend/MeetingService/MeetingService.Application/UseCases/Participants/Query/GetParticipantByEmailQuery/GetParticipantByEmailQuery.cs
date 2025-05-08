using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDtos;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public record GetParticipantByEmailQuery : IRequest<ParticipantWithMeetingDto>
{
    public Guid MeetingId { get; set; }
    public string Email { get; set; } = string.Empty;
}