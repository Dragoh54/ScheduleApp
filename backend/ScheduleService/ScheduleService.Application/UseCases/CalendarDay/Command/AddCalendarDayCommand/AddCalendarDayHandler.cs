using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.AddCalendarDayCommand;

public class AddCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<AddCalendarDayCommand, CalendarDayDto>
{
    public Task<CalendarDayDto> Handle(AddCalendarDayCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}