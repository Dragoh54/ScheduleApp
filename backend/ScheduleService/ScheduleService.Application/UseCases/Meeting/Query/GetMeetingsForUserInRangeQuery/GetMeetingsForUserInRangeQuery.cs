using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.Meeting.Query.GetMeetingsForUserInRangeQuery;

public record GetMeetingsForUserInRangeQuery : IRequest<IEnumerable<MeetingDto>>
{
    public Guid UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}