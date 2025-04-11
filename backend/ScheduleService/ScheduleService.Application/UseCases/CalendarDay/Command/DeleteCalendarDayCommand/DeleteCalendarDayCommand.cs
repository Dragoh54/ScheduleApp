using MediatR;
using ScheduleService.Application.Dto;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.DeleteCalendarDayCommand;

public class DeleteCalendarDayCommand : IRequest<CalendarDayDto>
{
    public Guid Id { get; set; }
}