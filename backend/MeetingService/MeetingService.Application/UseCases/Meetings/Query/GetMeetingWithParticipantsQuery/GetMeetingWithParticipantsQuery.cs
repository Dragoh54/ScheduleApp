using MediatR;
using MeetingService.Application.Dtos;
using MeetingService.Application.Dtos.MeetingDtos;

namespace MeetingService.Application.UseCases.Meetings.Query.GetMeetingWithParticipantsQuery;

public record GetMeetingWithParticipantsQuery : IRequest<MeetingDto>
{
    public Guid MeetingId { get; set; }
}