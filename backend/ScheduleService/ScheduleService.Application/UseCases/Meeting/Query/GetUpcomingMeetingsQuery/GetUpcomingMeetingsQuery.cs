using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetUpcomingMeetingsQuery;

public record GetUpcomingMeetingsQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid UserId { get; set; }
}