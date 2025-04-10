using MediatR;
using ScheduleService.Application.Dto;
using ScheduleService.DataAccess.Interfaces.UnitOfWork;

namespace ScheduleService.Application.UseCases.CalendarDay.Command.UpdateCalendarDayCommand;

public class UpdateCalendarDayHandler(
    IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCalendarDayCommand, CalendarDayDto>
{
    public Task<CalendarDayDto> Handle(UpdateCalendarDayCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}