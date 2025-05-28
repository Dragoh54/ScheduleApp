using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.ParticipantDto.Responses;

namespace MeetingService.Application.UseCases.Participants.Query.GetParticipantByEmailQuery;

public record GetParticipantByEmailQuery : IRequest<ParticipantWithMeetingResponseDto>
{
    public Guid MeetingId { get; set; }
    public string Email { get; set; } = string.Empty;
}