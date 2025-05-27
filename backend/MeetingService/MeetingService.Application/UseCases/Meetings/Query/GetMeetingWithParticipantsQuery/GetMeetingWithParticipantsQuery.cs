using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDto.Responses;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public record GetMeetingWithParticipantsQuery : IRequest<MeetingWithParticipantsResponseDto>
{
    public Guid MeetingId { get; set; }
}