using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetForPeriodCalendarDayQuery;

public class GetForPeriodCalendarDayQuery : IRequest<IEnumerable<CalendarDayDto>>
{
    
}