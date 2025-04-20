using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserOnDateQuery;

public record GetMeetingsForUserOnDateQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid UserId { get; set; }
    public DateTime Date { get; set; }
}