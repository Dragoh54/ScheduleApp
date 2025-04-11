using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.CalendarDay.Query.GetByDateCalendarDayQuery;

public class GetByDateCalendarDayQuery : IRequest<CalendarDayDto>
{
    public Guid UserId { get; set; }
    public DateOnly Date { get; set; }
}