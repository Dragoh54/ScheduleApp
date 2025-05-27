using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Responses;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantQuery;

public record GetParticipantQuery : IRequest<ParticipantWithMeetingResponseDto>
{
    public Guid UserId { get; set; }
    public Guid MeetingId { get; set; }
}