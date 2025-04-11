using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetForPeriodCalendarDayQuery;

public class GetForPeriodCalendarDayQuery : IRequest<IEnumerable<CalendarDayDto>>
{
    public Guid UserId { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
}